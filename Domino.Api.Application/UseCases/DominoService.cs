using Domino.Api.Application.Interfaces;
using Domino.Api.Infrastructure.Interfaces;
using System.Text;
using System.Threading;

namespace Domino.Api.Application.UseCases;

public class DominoService : IDominoService
{
    private readonly IRedisCache _redisCache; //in case of future use. Only suitable if less that 165 dominoes ((1024-34)/6)

    public DominoService(IRedisCache redisCache)
    {
        _redisCache = redisCache;
    }
    
    public async Task<List<string>> Sort(List<string> dominoes)
    {
        //List<string>? redisResult = await _redisCache.Get<List<string>>
        //    ($"{nameof(DominoService)}{nameof(Sort)}{Encoding.UTF8.GetBytes(string.Join("", dominoes))}");
        //if (redisResult != null) return redisResult;

        List<string> resultList = new ();
        List<string> notMatchedYet = new ();
        List<string> dominoesCopy;

        if (dominoes.Count == 2 && !dominoes[0].Order().SequenceEqual(dominoes[1].Order())) return resultList;

        for (int i = 0; i < dominoes.Count ; i++)
        {
            InitializeIteration(resultList, notMatchedYet, dominoesCopy = dominoes.ToList(), i);
            TrySort(dominoesCopy, notMatchedYet, resultList);
            if (CheckLastDomino(dominoesCopy, notMatchedYet, resultList, dominoes.Count))
            {
                //_redisCache.Set
                //    ($"{nameof(DominoService)}{nameof(Sort)}{Encoding.UTF8.GetBytes(string.Join("", dominoes))}",
                //Encoding.UTF8.GetBytes(string.Join("", resultList))).ConfigureAwait(false); //This tells the compiler to continue executing on a different thread pool thread after the method call completes, which can improve performance.However, it's important to note that this approach should only be used if you're certain that you don't need to await the result for any other reason. 

                return resultList;
            }
        }
        resultList.Clear();

        return resultList;
    }

    private static void TrySort(List<string> dominoes, List<string> notMatchedYet, List<string> resultList)
    {
        for (int i = 0; i < notMatchedYet.Count; i++)
        {
            if (resultList[resultList.Count - 1][2] == notMatchedYet[i][0])
            {
                resultList.Add(notMatchedYet[i]);
                notMatchedYet.RemoveAt(i);
                i--;
            }
            else if (resultList[resultList.Count - 1][2] == notMatchedYet[i][2])
            {
                resultList.Add(new string(notMatchedYet[i].Reverse().ToArray()));
                notMatchedYet.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < dominoes.Count - 1; i++)
        {
            if (resultList[resultList.Count - 1][2] == dominoes[i][0])
            {
                resultList.Add(dominoes[i]);
                dominoes.RemoveAt(i);

                TrySort(dominoes, notMatchedYet, resultList);
            }
            else if (resultList[resultList.Count - 1][2] == dominoes[i][2])
            {
                resultList.Add(new string(dominoes[i].Reverse().ToArray()));
                dominoes.RemoveAt(i);

                TrySort(dominoes, notMatchedYet, resultList);
            }
            if (dominoes.Count != 0)
            {
                notMatchedYet.Add(dominoes[i]);
                dominoes.RemoveAt(i);
            }
            i--;
        }
    }

    private static bool CheckLastDomino(List<string> dominoesCopy, List<string> notMatchedYet, List<string> resultList, int dominoesLength)
    {
        if (resultList.Count == dominoesLength - 1)
        {
            if (notMatchedYet.Count != 0)
            {
                if (resultList[0][0] == notMatchedYet[0][2])
                {
                    resultList.Add(notMatchedYet[0]);

                    return true;
                }
                if (resultList[0][0] == notMatchedYet[0][0])
                {
                    resultList.Add(new string(notMatchedYet[0].Reverse().ToArray()));

                    return true;
                }
            }
            if (dominoesCopy.Count != 0)
            {
                if (resultList[0][0] == dominoesCopy[0][2])
                {
                    resultList.Add(dominoesCopy[0]);

                    return true;
                }
                if (resultList[0][0] == dominoesCopy[0][0])
                {
                    resultList.Add(new string(dominoesCopy[0].Reverse().ToArray()));

                    return true;
                }
            }
        }
        return false;
    }

    private static void InitializeIteration
        (List<string> resultList, List<string> notMatchedYet, List<string> dominoesCopy, int actualIteration)
    {
        resultList.Clear();
        notMatchedYet.Clear();
        for (int j = actualIteration - 1; j >= 0; j--)
        {
            notMatchedYet.Add(dominoesCopy[0]);
            dominoesCopy.RemoveAt(0);
        }
        resultList.Add(dominoesCopy[0]);
        dominoesCopy.RemoveAt(0);
    }
}
