$(document).ready(function () {
    var exerciseIndex = 0;
    $(".exercise-item-add").on("click", function () {
        $.ajax({
            url: '/exercise/details/' + $(this).attr('data-exerciseid'),
            success: function (data) {
                $("#ExercisesContainer").
                    append("<tr><td><input type='hidden' name='Details[" + exerciseIndex + "].ExerciseId' Value='" + data.id + "'/>" +
                        "<input type='text' name='Details[" + exerciseIndex + "].NameExercise' Value='" + data.name + "' /></td>" +
                        "<td><input type='text' name='Details[" + exerciseIndex + "].SeriesCount' </td>" +
                        "<td><input type='text' name='Details[" + exerciseIndex + "].Repetitions' </td>" +
                        "<td><input type='text' name='Details[" + exerciseIndex + "].Break' </td></tr>");
                exerciseIndex++;
            }
        });
    });

    $(".btn-delete-detail").on("click", function () {
        var id = $(this).attr('data-detail');
        $("#detail-row-" + id).remove();
    })
});
