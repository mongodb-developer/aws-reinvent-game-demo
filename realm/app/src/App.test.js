import { render, screen } from "@testing-library/react";
import { winnerDiff } from "./App";

test("new leaderboard has null changes", () => {
  const result = winnerDiff(null, [
    { username: "a", score: 2 },
    { username: "b", score: 1 },
  ]);
  expect(result).toStrictEqual({
    a: { score: 2, change: null, index: 0 },
    b: { score: 1, change: null, index: 1 },
  });
});

test("score increase", () => {
  const result = winnerDiff(
    { a: { score: 2, index: 0 }, b: { score: 1, index: 1 } },
    [
      { username: "a", score: 3 },
      { username: "b", score: 1 },
    ]
  );
  expect(result).toStrictEqual({
    a: {
      score: 3,
      previousScore: 2,
      index: 0,
      previousIndex: 0,
      change: "inc",
    },
    b: { score: 1, previousScore: 1, index: 1, previousIndex: 1, change: null },
  });
});

test("position change", () => {
  const result = winnerDiff(
    { a: { score: 2, index: 0 }, b: { score: 1, index: 1 } },
    [
      { username: "b", score: 5 },
      { username: "a", score: 2 },
    ]
  );
  expect(result).toStrictEqual({
    b: { score: 5, previousScore: 1, index: 0, previousIndex: 1, change: "up" },
    a: {
      score: 2,
      previousScore: 2,
      index: 1,
      previousIndex: 0,
      change: "down",
    },
  });
});

test("position change", () => {
  const result = winnerDiff(
    { a: { score: 2, index: 0 }, c: { score: 1, index: 1 } },
    [
      { username: "b", score: 5 },
      { username: "a", score: 2 },
    ]
  );
  expect(result).toStrictEqual({
    b: {
      score: 5,
      previousScore: null,
      index: 0,
      previousIndex: null,
      change: "up",
    },
    a: {
      score: 2,
      previousScore: 2,
      index: 1,
      previousIndex: 0,
      change: "down",
    },
  });
});
