using System.Web.Mvc;

namespace G.Giedryte_Assignment.Controllers
{
    public class JumpController : Controller
    {
        public JsonResult GetJumpObject(int[] array)
        {
            MinimumJump minJump = new MinimumJump();
            int arrayLength = array.Length;
            int[] jumpPositionIndexList = new int[array.Length];
            int countOfJumps;
            jumpPositionIndexList = minJump.jumpPositionIndexList(array, arrayLength, out countOfJumps);

            string path = "end";
            int index = arrayLength;
            string resultMessage = "";

            if (countOfJumps == int.MaxValue || countOfJumps == 0)
                resultMessage = "Cannot reach the last element for array :(";
            else
            {
                while (index > 0)
                {
                    index = jumpPositionIndexList[index];
                    int val = array[index];
                    path = $"(A[{index}] = {val}) -> " + path;
                }

                resultMessage = $"Minimum jumps to reach the last element for an array is {countOfJumps}. Most efficient path: {path}";
            }

            return Json(new { Result = resultMessage });
        }

        public class MinimumJump
        {
            public int[] jumpPositionIndexList(int[] array, int arrayLength, out int countOfJumps)
            {
                int[] jumps = new int[arrayLength + 1]; // stores the amount of jumps necessary to reach that maximal reachable position
                int[] posOfJumps = new int[arrayLength + 1]; // stores positions (indexes of array) from which these jumps were made
                int i, j;

                // initial values
                jumps[0] = 0;
                posOfJumps[0] = 0;
                countOfJumps = 0;

                // if there are 0 elements in array or first element in array is equal to 0, cannot reach the end of array
                if (arrayLength == 0 || array[0] == 0)
                {
                    return jumps;
                }

                // jumps[i] indicates the minimum number of jumps needed to reach array[i] from array[0]
                for (i = 1; i <= arrayLength; i++)
                {
                    jumps[i] = int.MaxValue;
                    for (j = 0; j < i; j++)
                    {
                        if (i <= j + array[j] && jumps[j] != int.MaxValue)
                        {
                            if (jumps[i] >= jumps[j] + 1)
                            {
                                jumps[i] = jumps[j] + 1;
                                posOfJumps[i] = j;
                            }
                        }
                    }
                }

                countOfJumps = jumps[arrayLength - 1];
                return posOfJumps;
            }
        }
    }
}