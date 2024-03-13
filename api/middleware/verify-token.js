const jwt = require('jsonwebtoken');

function verifyToken(req, res, next) {
  const token = req.headers['authorization'];
  if (!token) return res.status(403).send({ auth: false, message: 'Токен не предоставлен.' });

  jwt.verify(token, process.env.JWT_SECRET, function(err, decoded) {
    if (err) return res.status(500).send({ auth: false, message: 'Не удалось аутентифицировать токен.' });

    req.userId = decoded.id;
    next();
  });
}

module.exports = verifyToken