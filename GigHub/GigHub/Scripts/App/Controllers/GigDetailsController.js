var GigDetailsController = function (followingServices) {
    //Global Var to Done Method
    var followBtn;
    var init = function () {
        $(".btn-follow").on("click", toggleAttendancesBtn);
    }

    //this make decision of choose which Api function will calling
    var toggleAttendancesBtn = function (e) {

        followBtn = $(e.target);

        var artistInfromObjec = {
            artistId: followBtn.attr("data-artist-id"),
            done: done,
            fail: fail
        }

        if (followBtn.hasClass("btn-default")) {
            followingServices.Follow(artistInfromObjec)
        }
        else {
            followingServices.UnFollow(artistInfromObjec);
        }
    };

    var done = function () {
        debugger;
        var storeVaildText = (followBtn.text() == "Follow") ? "Following" : "Follow";

        followBtn
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(storeVaildText);
    }

    var fail = function () {
        alert("someThing Wrong Happen");
    }

    return {
        init: init
    }

}(FollowingServices);
