using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
	public class SearchDataList
	{
        public IEnumerable<Data> Search(string searchKey, IEnumerable<Data> dataList)
        {
            var retList = new List<Data>();

            if (dataList != null)
            {
                foreach (Data data in dataList)
                {
                    // Check if searchKey is present in the value (case-insensitive)
                    if (data.Key.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        retList.Add(data);
                    }
                }
            }

            return retList;
        }

    }
}

