exports = () => {
  const scores = context.services
    .get("mongodb-atlas")
    .db("demo_games")
    .collection("scores");

  return scores.find({}).sort({ score: -1, name: 1 }).limit(10);
};
