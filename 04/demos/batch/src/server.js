const { Client } = require('pg')
const client = new Client()

const sql = 'UPDATE "public"."products" SET price = price * $1'
const factor = [process.env.PRICE_FACTOR]

client.connect()
client.query(sql, factor, (err, res) => {
  if (err) {
    console.log(err.stack)
  } 
  else {
    console.log(`Prices updated by factor: ${process.env.PRICE_FACTOR}`) 
  } 
  process.exit();  
})