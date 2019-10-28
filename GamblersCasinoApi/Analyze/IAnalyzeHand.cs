using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasinoApi.Analyze
{
    public interface IAnalyzeHand
    {
        List<Player> FindBestHand(List<Player> players, int highCardIndex);
    }
}
