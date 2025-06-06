using System.Reflection.PortableExecutable;
using Telegram.Bot.Types;

namespace Miri_sDiceRoller
{
    internal class Roller
    {
        private Random _random = new();
        private Parser parser = new();
        private int _dicePool = 0;
        private int _dice = 1;
        private int _difficult = 0;
        private int _criticalFail = 1;
        private int _criticalSuccess = 0;
        private bool _will = false;
        private bool _spec = false;

        public string NewRoll(string message)
        {

            var rollParams = parser.Parse(message);
            _dicePool = rollParams["DicePool"];
            _dice = rollParams["Dice"];
            _difficult = rollParams["Difficult"];
            _criticalFail = rollParams["CritFail"];
            _criticalSuccess = rollParams["CritSuccess"];
            _will = rollParams["Will"] > 0;
            _spec = rollParams["Spec"] > 0;
            return Roll();
        }

        public string Reroll()
        {
            return Roll();
        }

        private string Roll()
        {
            int res = 0;
            int succ = 0;
            int critSucc = 0;
            int critFail = 0;
            List<int> results = new List<int>();

            int temp;
            for (int x = 0; x < _dicePool; x++) 
            {
                temp = _random.Next(1, _dice+1);
                results.Add(temp);
                if (temp >= _criticalSuccess)
                    critSucc++;
                else if (temp <= _criticalFail)
                    critFail++;
                if (temp >= _difficult)
                {
                    succ++;
                }
            }

            if (_spec)
            {
                if (critFail >= critSucc)
                {
                    res = succ - (critSucc - critFail);
                }

                for (int x = 0; critSucc - x > critFail; x++)
                {
                    temp = _random.Next(1, _dice + 1);
                    results.Add(temp);
                    if (temp >= _criticalSuccess)
                        critSucc++;
                    if (temp >= _difficult)
                    {
                        succ++;
                    }
                }
            }
            res = succ - critFail;
            string resultsStr = "";
            foreach (int x in results)
            {
                resultsStr += x.ToString() + " ";
            }
            var result = $"Итог: {res} \n{succ} Усп.\n{critSucc} Крит. усп.\n{critFail} Крит. пров.\nВсе броски: {resultsStr}";
            return result;
        }
    }
}
