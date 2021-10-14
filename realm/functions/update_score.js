exports = async function(changeEvent) {
  /*
    A Database Trigger will always call a function with a changeEvent.
    Documentation on ChangeEvents: https://docs.mongodb.com/manual/reference/change-events/

    Access the _id of the changed document:
    const docId = changeEvent.documentKey._id;

    Access the latest version of the changed document
    (with Full Document enabled for Insert, Update, and Replace operations):
    const fullDocument = changeEvent.fullDocument;

    const updateDescription = changeEvent.updateDescription;

    See which fields were changed (if any):
    if (updateDescription) {
      const updatedFields = updateDescription.updatedFields; // A document containing updated fields
    }

    See which fields were removed (if any):
    if (updateDescription) {
      const removedFields = updateDescription.removedFields; // An array of removed fields
    }

    Functions run by Triggers are run as System users and have full access to Services, Functions, and MongoDB Data.

    Access a mongodb service:
    const collection = context.services.get("mongodb-atlas").db("demo_games").collection("players");
    const doc = collection.findOne({ name: "mongodb" });

    Note: In Atlas Triggers, the service name is defaulted to the cluster name.

    Call other named functions if they are defined in your application:
    const result = context.functions.execute("function_name", arg1, arg2);

    Access the default http client and execute a GET request:
    const response = context.http.get({ url: <URL> })

    Learn more about http client here: https://docs.mongodb.com/realm/functions/context/#context-http
  */
  const id = changeEvent.documentKey._id;
  const players = context.services.get("mongodb-atlas").db("demo_games").collection("players");
  const scores = context.services.get("mongodb-atlas").db("demo_games").collection("scores");
  
  if (changeEvent.updateDescription) {
    const updatedFields = changeEvent.updateDescription.updatedFields;
    if (Object.keys(updatedFields).some((key) => /games\..*\.high_score/.test(key))) {
      const highScoreDocs = await players.aggregate([
        {
          $match: {
            _id: id
          }
        },
        {
          $project: {
            name: 1,
            total_score: {
              $sum: {
                $map: {
                  input: { $objectToArray: "$games" },
                  as: "game",
                  in: "$$game.v.high_score",
                }
              }
            }
          }
        },
      ]).toArray();
      const highScoreDoc = highScoreDocs[0];
      console.log(`Set ${highScoreDoc.name} to ${highScoreDoc.total_score}`);
      scores.updateOne(
        { username: highScoreDoc.name },
        {$set: { username: highScoreDoc.name, score: highScoreDoc.total_score, }},
        { upsert: true });
      players.updateOne(
        { _id: highScoreDoc._id },
        { $set: { total_score: highScoreDoc.total_score }});
    }
  }
};
