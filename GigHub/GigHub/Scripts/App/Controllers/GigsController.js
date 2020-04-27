var GigsController = function (attendanceService) {
    // Public Var
    var button;

    //Private Part 
    var initGigs = function () {
        $(".attend-gig-btn").on("click", toggleAttendancesBtn);
    }

    var toggleAttendancesBtn = function (e) {
        button = $(e.target);

        var gigInformObject = {

            gigId: button.attr("data-gigid"),
            done: done,
            fail: fail
        };

        if (button.hasClass("btn-default")) {
            attendanceService.createAttendances(gigInformObject);

        } else {
            attendanceService.deleteAttendances(gigInformObject);
        }
    }

    var done = function () {
        //Toggel Button Text
        var btnText = (button.text() == "Going") ? "Going ?" : "Going";
        button
            .toggleClass("btn-default")
            .toggleClass("btn-info")
            .text(btnText);
    };

    var fail = function () {
        alert("Something Wrong Happen");
    };


    //Public Part
    return {
        initGigs: initGigs
    }

}(AttendanceService);


//var AttendanceService = function () {

//    var createAttendances = function (gigInformObject) {

//        //"GigId" is a Name Of Dto Object Of GigId
//        //Any Different Name it will not make Binding between them
//        $.post("/api/Attendances", { GigId: gigInformObject.gigId })
//            .done(gigInformObject.done)
//            .fail(gigInformObject.fail);
//    };

//    var deleteAttendances = function (gigInformObject) {
//        $.ajax({
//            url: "/api/Attendances/" + gigInformObject.gigId,
//            method: "DELETE"
//        })
//            .done(gigInformObject.done)
//            .fail(gigInformObject.fail);
//    };

//    return {
//        createAttendances: createAttendances,
//        deleteAttendances: deleteAttendances
//    }
//}();