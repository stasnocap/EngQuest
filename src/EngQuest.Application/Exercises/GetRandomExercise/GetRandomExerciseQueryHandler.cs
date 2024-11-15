﻿using System.Diagnostics.CodeAnalysis;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Exercises.GetExercise;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Lessons;

namespace EngQuest.Application.Exercises.GetRandomExercise;

[SuppressMessage("Security", "CA5394:Do not use insecure randomness")]
public class GetRandomExerciseQueryHandler(ILessonRepository _lessonRepository, ExerciseConverter _exerciseConverter) : IQueryHandler<GetRandomExerciseQuery, ExerciseResponse>
{
    public async Task<Result<ExerciseResponse>> Handle(GetRandomExerciseQuery request, CancellationToken cancellationToken)
    {
        Lesson? lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);

        if (lesson is null)
        {
            return Result.Failure<ExerciseResponse>(LessonErrors.NotFound);
        }
        
        LessonExercise lessonExercise = lesson.Exercises[Random.Shared.Next(lesson.Exercises.Count)];

        ExerciseResponse exerciseResult = await _exerciseConverter.ConvertAsync(lessonExercise.Exercise, lesson, cancellationToken);

        return exerciseResult;
    }
}
