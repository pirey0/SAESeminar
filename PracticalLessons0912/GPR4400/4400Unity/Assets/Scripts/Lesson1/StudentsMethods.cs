
/*
public static class StudentMethods
{


    //Luca
    public static void HelloWorld()
    {
        Debug.Log("Hello World");
    }

    public static float Clamp(float f, float min, float max)
    {
        return Mathf.Min(Mathf.Max(f, min), max);
    }

    //Hoang
    Public float Clamp(float Max, float Min, float CurrentNum)
    {

        if (CurrentNum >= Max)
        {
            CurrentNum = Max;
        }
        if (CurrentNum <= Min)
        {
            CurrentNum = Min;
        }
        yield return new CurrentNum;
    }



    //Youssef OK
    private float Clamp(float value, float min, float max)
    {
        if (value > max)
            value = max;
        else if (value < min)
            value = min;

        return value;
    }








    // Egor OK
    float Clamp(float input, float min, float max)
    {
        if (input <= min)
            return min;
        else if (input >= max)
            return max;
        else
            return input;
    }

    //Flo OK
    public float clamp(float input, float min, float max)
    {
        if (input < min)
            return min;

        else if (input > max)
            return max;

        else
            return input;
    }

    public float remap(float input, float minInput, float maxInput, float minOutput, float maxOutput)
    {
        input = clamp(input, minInput, maxInput);

        float distance = abs(maxInput - minInput)
    

    float respPos = (input - minInput) / distance  //decimal value between 0 and 1; tells us where, respectively to minInput and maxInput; the input is


    float respOut = respPos * (maxOutput - minOutput); //get respPos percent of (the value between minOutput and maxOutput which we know is positive)
        float result = respOut + minOutput;
        return result;

    }



    //Pedro OK
    //Clamp(5, 0, 1);
    float Clamp(float value, float min, float max)
    {
        if (value < min)
            return min;
        else if (value < max)
            return max;
        else
            return value;
    }


    //Hayoon OK
    void Clamp(float value, float min, float max)
    {

        if (value < min)
            value = min;

        if (value > max)
            value = max;

        return value;
    }

    //Dustin OK
    pubic float Clamp(float input, float min, float max)
    {
        if (input < min)
        {
            input = min;
        }

        if (input >= max)
        {
            input = max;
        }

        retrun input;
    }
    */