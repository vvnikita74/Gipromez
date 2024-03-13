require('dotenv').config();
const express = require('express')
const jwt = require('jsonwebtoken')
const bcrypt = require('bcryptjs')
const { Pool } = require('pg')


const pool = new Pool({
  user: process.env.DB_USER,
  host: process.env.DB_HOST,
  database: process.env.DB_DATABASE,
  password: process.env.DB_PASSWORD,
  port: process.env.DB_PORT,
})


const app = express()
app.use(express.json())


const PORT = process.env.PORT || 3000

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

app.listen(PORT, () => console.log(`Server is running on port ${PORT}`))
