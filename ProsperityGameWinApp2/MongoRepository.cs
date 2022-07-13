using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProsperityGameWinApp2
{
    public class MongoRepository
    {
        private readonly IMongoDatabase _db;
        public MongoRepository(string connectionString = "mongodb://localhost:27017")
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            var client = new MongoClient(settings);
            _db = client.GetDatabase("ProsperityDb");
        }

        public void AddSuccess(Success record)
        {
            var collection = _db.GetCollection<Success>("Success");
            collection.InsertOneAsync(record);
        }

        public List<Success> GetSuccessList()
        {
            var collection = _db.GetCollection<Success>("Success");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }

        public void Insert(ProsperityStatus record)
        {
            var collection = _db.GetCollection<ProsperityStatus>("ProsperityStats");
            collection.InsertOneAsync(record);
        }

        public ProsperityStatus Get()
        {
            var collection = _db.GetCollection<ProsperityStatus>("ProsperityStats");
            var filter = new BsonDocument();
            var result = collection.Find(filter).ToList();

            return result.FirstOrDefault();
        }

        public double GetTotalCredit()
        {
            var collection = _db.GetCollection<ProsperityStatus>("ProsperityStats");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            double result = 0;
            foreach (var item in list)
            {
                result += item.CreditValue;
            }

            return result;
        }

        public List<ProsperityStatus> GetIncomeList()
        {
            var collection = _db.GetCollection<ProsperityStatus>("ProsperityStats");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }

        public DateTime GetStartDate()
        {
            var list = GetIncomeList();
            if (list.Any())
            {
                var lastDate = list.OrderByDescending(i => i.Date).First().Date;
                var days = (DateTime.Now - lastDate).TotalDays;
                if (days > 2)
                {
                    UnActiveGames();
                }
                else
                {
                    return GetActiveStartGame();
                }
            }

            var starGame = new StartGame()
            {
                Date = DateTime.Now.AddDays(-1),
                Id = ObjectId.GenerateNewId(),
                IsActive = true
            };
            StartGame(starGame);

            return starGame.Date;
        }

        private DateTime GetActiveStartGame()
        {
            var collection = _db.GetCollection<StartGame>("StartGames");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();

            if (list.Any())
            {
                return list.OrderByDescending(i => i.Date).First(i => i.IsActive).Date;
            }

            var starGame = new StartGame()
            {
                Date = DateTime.Now.AddDays(-1),
                Id = ObjectId.GenerateNewId(),
                IsActive = true
            };
            StartGame(starGame);

            return starGame.Date;
        }

        public void StartGame(StartGame record)
        {
            var collection = _db.GetCollection<StartGame>("StartGames");
            collection.InsertOneAsync(record);
        }

        public void UnActiveGames()
        {
            var collection = _db.GetCollection<StartGame>("StartGames");
            var updates = Builders<StartGame>.Update.Set("IsActive", false);
            var filter = Builders<StartGame>.Filter.Where(i => true);

            collection.UpdateMany(filter, updates, null);
        }

        public void AddGoal(string title, Category category)
        {
            var record = new Goal()
            {
                Id = new ObjectId(),
                Date = DateTime.Now.Date,
                Title = title,
                Category = category,
                Targets = new List<Target>()
            };
            var collection = _db.GetCollection<Goal>("Goals");
            collection.InsertOneAsync(record);
        }
        public List<Goal> GetGoalsList()
        {
            var collection = _db.GetCollection<Goal>("Goals");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }

        public void AddTarget(Goal goal, string title)
        {
            var record = new Target() 
            {
                Id = new ObjectId(),
                Date = DateTime.Now.Date,
                Title = title,
                GoalId = goal.Id,
                Category = goal.Category,
                Tasks = new List<UserTask>()
            };
            var collection = _db.GetCollection<Target>("Targets");
            collection.InsertOneAsync(record);
        }
        public List<Target> GetTargetsList()
        {
            var collection = _db.GetCollection<Target>("Targets");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }

        public void AddTask(Target target, string title, Priority priority)
        {
            var record = new UserTask() 
            {
                Id = new ObjectId(),
                Date = DateTime.Now.Date,
                Title = title,
                Priority = priority,
                TargetId = target.Id,
                Category = target.Category,
                Activities = new List<UserActivity>()
            };
            var collection = _db.GetCollection<UserTask>("UserTasks");
            collection.InsertOneAsync(record);
        }
        public List<UserTask> GetUserTasksList()
        {
            var collection = _db.GetCollection<UserTask>("UserTasks");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }

        public void AddUserActivity(ObjectId taskId, string title, double time)
        {
            var record = new UserActivity() 
            {
                Id = new ObjectId(),
                Date = DateTime.Now.Date,
                Title = title,
                Time = time,
                TaskId = taskId
            };
            var collection = _db.GetCollection<UserActivity>("UserActivities");
            collection.InsertOneAsync(record);
        }
        public List<UserActivity> GetUserActivitysList()
        {
            var collection = _db.GetCollection<UserActivity>("UserActivities");
            var filter = new BsonDocument();
            var list = collection.Find(filter).ToList();
            return list;
        }
    }
}
