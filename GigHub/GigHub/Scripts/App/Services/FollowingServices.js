var FollowingServices = function () {

    //Tow Functions Resive Object with All infromations to complete work
    var Follow = function (artistInfromObjec) {
        debugger;
        $.post("/api/Followers/", { FolloweeId: artistInfromObjec.artistId })
            .done(artistInfromObjec.done)
            .fail(artistInfromObjec.fail);
    }

    var UnFollow = function (artistInfromObjec) {
        $.ajax({
            url: "/api/Followers/" + artistInfromObjec.artistId,
            method: "DELETE",
        })
            .done(artistInfromObjec.done)
            .fail(artistInfromObjec.fail);
    }

    return {
        Follow: Follow,
        UnFollow: UnFollow
    }

}();