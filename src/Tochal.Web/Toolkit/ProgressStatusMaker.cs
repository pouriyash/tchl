/*
using System;


namespace Research.City.Admin.Toolkit
{
    public static class ProgressStatusMaker
    {
        public static ProgressStatus Build(int _totalSteps, int _completedSteps)
        {
            var _progressPercentage = (int) Math.Round((double)(_completedSteps * 100) / _totalSteps);
            var _isComplete = _completedSteps == _totalSteps;

            var result = new ProgressStatus
            {
                CompletedSteps = _completedSteps,
                IsComplete = _isComplete,
                ProgressPercentage = _progressPercentage,
                TotalSteps = _totalSteps,
            };

            return result;
        }
    }
}
*/