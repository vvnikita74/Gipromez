require('dotenv').config();
const express = require('express')
const jwt = require('jsonwebtoken')
const bcrypt = require('bcryptjs')
const { Pool } = require('pg')

const verifyToken = require('./middleware/verify-token')

const PORT = process.env.PORT || 3000

const pool = new Pool({
  user: process.env.DB_USER,
  host: process.env.DB_HOST,
  database: process.env.DB_DATABASE,
  password: process.env.DB_PASSWORD,
  port: process.env.DB_PORT,
})

const app = express()
app.use(express.json())


app.post('/register', async (req, res) => {
  const { username, password } = req.body;
  const hashedPassword = await bcrypt.hash(password, 8);

  try {
    const result = await pool.query(
      'INSERT INTO users (username, password) VALUES ($1, $2) RETURNING id',
      [username, hashedPassword]
    );
    res.status(201).send({ userId: result.rows[0].id });
  } catch (error) {
    console.log(error)
    res.status(500).send('Ошибка при регистрации пользователя');
  }
});


app.post('/login', async (req, res) => {
  const { username, password } = req.body

  try {
    const result = await pool.query('SELECT * FROM users WHERE username = $1', [username])
    if (result.rows.length > 0) {
      const user = result.rows[0]

      const passwordIsValid = await bcrypt.compare(password, user.password)
      if (!passwordIsValid) return res.status(401).send('Неверный пароль.')

      const token = jwt.sign({ id: user.id }, process.env.JWT_SECRET, {
        expiresIn: 86400, // 24 часа
      })

      res.status(200).send({ auth: true, token })
    } else {
      res.status(404).send('Пользователь не найден.')
    }
  } catch (error) {
    res.status(500).send('Ошибка при входе в систему')
  }
})


app.get('/files', verifyToken, async (req, res) => {
  try {
    const result = await pool.query('SELECT file_name, file_path FROM files')
    res.json(result.rows)
  } catch (error) {
    console.log(error)
    res.status(500).send('Ошибка сервера при получении файлов')
  }
})


app.listen(PORT, () => console.log(`Server is running on port ${PORT}`))
