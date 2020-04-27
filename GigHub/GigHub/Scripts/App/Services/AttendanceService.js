var AttendanceService = function () {

    var createAttendances = function (gigInformObject) {

        //"GigId" is a Name Of Dto Object Of GigId
        //Any Different Name it will not make Binding between them
        $.post("/api/Attendances", { GigId: gigInformObject.gigId })
            .done(gigInformObject.done)
            .fail(gigInformObject.fail);
    };

    var deleteAttendances = function (gigInformObject) {
        $.ajax({
            url: "/api/Attendances/" + gigInformObject.gigId,
            method: "DELETE"
        })
            .done(gigInformObject.done)
            .fail(gigInformObject.fail);
    };

    return {
        createAttendances: createAttendances,
        deleteAttendances: deleteAttendances
    }
}();