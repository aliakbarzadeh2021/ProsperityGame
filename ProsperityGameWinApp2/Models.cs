using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProsperityGameWinApp2
{
    public class Goal
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public List<Target> Targets { get; set; }

        public Category Category { get; set; }

        public DateTime Date { get; set; }
    }

    public enum Category
    {
        Financial,
        Body,
        Education,
        Attractive,
        Career,
        Health,
        Personality,
        Relational,
        Skill,
        Spiritual
    }

    public class Target
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public List<UserTask> Tasks { get; set; }

        public DateTime Date { get; set; }
        public ObjectId GoalId { get; internal set; }
        public Category Category { get; internal set; }
    }

    public class UserTask
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public List<UserActivity> Activities { get; set; }

        public Priority Priority { get; set; }

        public DateTime Date { get; set; }
        public ObjectId TargetId { get; internal set; }
        public Category Category { get; internal set; }
    }

    public enum Priority
    {
        Low=1,
        Normal,
        Important,
        High,
        Urgent
    }

    public class UserActivity
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public double Time { get; set; }

        public DateTime Date { get; set; }
        public ObjectId TaskId { get; internal set; }
    }

    public class ProsperityStatus
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double CreditValue { get; set; }

        public int DaysFromStart { get; set; }
    }

    public class StartGame
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

    }

    public class Success
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public SuccessType SuccessType { get; set; }

    }

    public enum SuccessType
    {
        SuccessStory,
        Belief,
        Confidence
    }
}
