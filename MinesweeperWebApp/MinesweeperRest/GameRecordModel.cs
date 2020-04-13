namespace MinesweeperRest
{
    public class GameRecordModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Time { get; set; }

        public GameRecordModel(int iD, string user, string time)
        {
            ID = iD;
            User = user;
            Time = time;
        }
    }
}