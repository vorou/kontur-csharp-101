static long Decode(string message)
{
	long badData = -1;

	try
	{
		string makedString = MakeCorrectString(message);
		return RemainderOfTheDevision(makedString);
	}
	catch
	{
		return badData;
	}
}

static string MakeCorrectString(String str)
{
	string makedString = "";

	string tmpMessage = str.Substring(1, str.Length-2);
    string [] tmpSplit = tmpMessage.Split('-');

    for (int i = 0; i < tmpSplit.Length; i++)
    {
       makedString += tmpSplit[i];
    }
	return makedString;
}

static long RemainderOfTheDevision(string tmpMessage)
{
	int devider = 1024;

	long number = long.Parse(tmpMessage);
	long middleAnswer = number % devider;
	return middleAnswer;
}
