using MongoDB.Bson;
using MongoDB.Driver;
//using Portocred.Data.Mongo.Entities;
using System.Linq;

namespace FrameWork.Util.Banco
{
    class ConexaoMongoDB
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public ConexaoMongoDB()
        {
            _client = new MongoClient("Conection_String");
            _database = _client.GetDatabase("h2h");
        }

        //public string _getParceiro(int idParceiro)
        //{
        //    var collection = _database.GetCollection<ParceiroCallback>("Collection"); // produtoscdc, parametros, cliente, feriados
        //    var mbd = collection.AsQueryable().Where(x => x.idCollection == idCollection).FirstOrDefault();
        //    return mbd.ToJson();
        //}

    }
}
