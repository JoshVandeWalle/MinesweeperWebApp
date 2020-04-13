using System.Runtime.Serialization;

namespace Rest
{
    /*
     * This class is an object model used to store game records after a game is won
     */
    [DataContract]
    public class GameRecordModel
    {
        // the game ID
        [DataMember]
        public int ID { get; set; }
        // the user who played the game
        [DataMember]
        public string User { get; set; }
        // the difficulty the game was played on
        [DataMember]
        public int Difficulty { get; set; }
        // the time it took the user to win
        [DataMember]
        public string Time { get; set; }

        /*
         * non-default constructor initializes object state
         * @param iD the game ID
         * @param user the user who won the game
         * @param difficulty the difficulty the game was played on
         * @param time the time it took the user to win
         */
        public GameRecordModel(int iD, string user, int difficulty, string time)
        {
            ID = iD;
            User = user;
            Difficulty = difficulty;
            Time = time;
        }
    }
}