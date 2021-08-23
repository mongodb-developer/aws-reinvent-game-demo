using System;
using System.Collections.Generic;
using Realms;

public class Player : RealmObject {

    [PrimaryKey]
    [MapTo("_id")]
    public string Id { get; set; }
    [MapTo("email")]
    public string Email { get; set; }
    [MapTo("games")]
    public Player_games Games { get; set; }
    [MapTo("leafs")]
    public int? Leafs { get; set; }
    [MapTo("total_score")]
    public int? TotalScore { get; set; }

}

public class Player_games : EmbeddedObject {

    [MapTo("change_streams")]
    public Player_games_change_streams ChangeStreams { get; set; }
    [MapTo("dance_dance")]
    public Player_games_dance_dance DanceDance { get; set; }
    [MapTo("target_practice")]
    public Player_games_target_practice TargetPractice { get; set; }

}

public class Player_games_change_streams : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

}

public class Player_games_dance_dance : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

}

public class Player_games_target_practice : EmbeddedObject {

    [MapTo("high_score")]
    public int? HighScore { get; set; }
    [MapTo("total_plays")]
    public int? TotalPlays { get; set; }

}