using System;
using System.Collections.Generic;
using Realms;

public class PlayerModel : RealmObject {

    [PrimaryKey]
    [MapTo("_id")]
    public string Id { get; set; }
    [MapTo("email")]
    public string Email { get; set; }
    [MapTo("games")]
    public PlayerModel_games Games { get; set; }
    [MapTo("leafs")]
    public int? Leafs { get; set; }
    [MapTo("total_score")]
    public int? TotalScore { get; set; }
    [MapTo("x")]
    public double? X { get; set; }
    [MapTo("y")]
    public double? Y { get; set; }

    public PlayerModel() { }

    public PlayerModel(string id, string email) {
        this.Id = id;
        this.Email = email;
        this.Leafs = 0;
        this.TotalScore = 0;
        this.X = -120.0;
        this.Y = -142.0;
        this.Games = new PlayerModel_games();
    }

}

public class PlayerModel_games : EmbeddedObject {

    [MapTo("change_streams")]
    public PlayerModel_games_change_streams ChangeStreams { get; set; }
    [MapTo("dance_dance")]
    public PlayerModel_games_dance_dance DanceDance { get; set; }
    [MapTo("target_practice")]
    public PlayerModel_games_target_practice TargetPractice { get; set; }
    [MapTo("fishing")]
    public PlayerModel_games_fishing Fishing { get; set; }

    public PlayerModel_games() {
        this.ChangeStreams = new PlayerModel_games_change_streams();
        this.DanceDance = new PlayerModel_games_dance_dance();
        this.TargetPractice = new PlayerModel_games_target_practice();
        this.Fishing = new PlayerModel_games_fishing();
    }

}

public class PlayerModel_games_change_streams : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

    public PlayerModel_games_change_streams() {
        this.HighScore = 0;
        this.TotalPlays = 0;
    }

}

public class PlayerModel_games_dance_dance : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

    public PlayerModel_games_dance_dance() {
        this.HighScore = 0;
        this.TotalPlays = 0;
    }

}

public class PlayerModel_games_target_practice : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

    public PlayerModel_games_target_practice() {
        this.HighScore = 0;
        this.TotalPlays = 0;
    }

}

public class PlayerModel_games_fishing : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

    public PlayerModel_games_fishing() {
        this.HighScore = 0;
        this.TotalPlays = 0;
    }

}