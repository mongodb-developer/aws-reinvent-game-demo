/**
 * Synchronize the score data stored in players to scores.
 *
 * **WARNING** this operation will drop and recreate the scores collection!
 */

const scores = db.scores;
const players = db.player_data;

const cleanup_stage = {
  $unset: "games.fake_game",
};

const project_stage = {
  $project: {
    username: "$name",
    score: {
      $sum: {
        $map: {
          input: { $objectToArray: "$games" },
          as: "game",
          in: "$$game.v.high_score",
        },
      },
    },
  },
};

const merge_stage = {
  $merge: {
    into: "scores",
    on: "username",
    whenMatched: "replace",
  },
};

scores.drop();
scores.createIndex({ username: 1 }, { unique: true });
players.aggregate([cleanup_stage]);
players.aggregate([project_stage, merge_stage]);
