exports = () => {
  const scores = context.services
    .get("mongodb-atlas")
    .db("mongo_world_demo")
    .collection("scores");

  return scores.find({}).sort({ score: -1, name: 1 }).limit(10);
};
