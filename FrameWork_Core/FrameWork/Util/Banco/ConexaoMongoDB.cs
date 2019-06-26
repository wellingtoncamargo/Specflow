using MongoDB.Bson;
using MongoDB.Driver;
//using Portocred.Data.Mongo.Entities;
using System.Linq;

namespace FrameWork.Util.Banco
{
    class ConexaoMongoDB
    {

          //"ConnectionString": "mongodb+srv://h2h-rw:VE7AIP3EZ8bTqNpQ@aws-psd-develop-oc8tg.mongodb.net/h2h?retryWrites=true",
          //"Database": "h2h"       
        
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public ConexaoMongoDB()
        {
            _client = new MongoClient("mongodb+srv://h2h-rw:VE7AIP3EZ8bTqNpQ@aws-psd-develop-oc8tg.mongodb.net/h2h?retryWrites=true");
            _database = _client.GetDatabase("h2h");
        }

        //public string _getParceiro(int idParceiro)
        //{
        //    var collection = _database.GetCollection<ParceiroCallback>("parceiro_callback"); // produtoscdc, parametros, cliente, feriados
        //    var mbd = collection.AsQueryable().Where(x => x.idParceiro == idParceiro).FirstOrDefault();
        //    return mbd.ToJson();
        //}

    }
}
