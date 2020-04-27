
//Controller For Gigs 
//IIFE function to Attendances gig (Going)
//invok Controller Functionsss 
GigsController.initGigs();

GigDetailsController.init();


// function to handel "?" in gogin btn
(function () {
    var btnsGoing = document.querySelectorAll(".details button");
    if (btnsGoing !== null || btnsGoing === undefined) {

        btnsGoing.forEach(function (btn) {
            if ($(btn).hasClass("btn-info")) {
                btn.removeChild(btn.lastChild);
            }
        });
    }
})();





(function () {
    /////////
    //cancel Gig Function 
    //////
    $(".deleteGigBtn").on("click", function () {
        var link = $(this);
        //bootbox confirm
        //important note Don't Initializ Any Var In BootBox Scop
        //bootbox.confirm("Are You sure You Want to Delete this Gig ?", function (result) {
        //});

        var dialog = bootbox.dialog({
            message: "<h2>Are You sure You Want to Delete this Gig</h2>",
            size: 'large',
            buttons: {
                no: {
                    label: "No",
                    className: 'btn-default',
                    callback: function () {
                        //close Dialog
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className: 'btn-danger',
                    callback: function () {
                        //Use Ajax Here Because Request Is a Delete
                        $.ajax({
                            url: "/api/Gigs/" + link.attr("data-gig-id"),
                            method: "DELETE",
                        })
                            .done(function () {
                                link.parents("li").fadeOut("slow", function () {
                                    //then remove from Dom 
                                    //link.parents("li").remove();
                                    $(this).remove();
                                });
                            })
                            .fail(function (data) {
                                alert("SomeThing Failed")
                            })
                    }
                }

            }
        });


    });

})();

(function () {
    //Get All New Notifications ==========
    $.getJSON("/api/Notifications/GetNewNotifications", function (date) {
        if (date.count.length > 0) {
            $(".js-notification-count").text(date.count.length)
                .removeClass("hide")
                .addClass("animated shake");
        }

        //Call popover Plugin
        $('.notification').popover({
            html: true,
            title: "Noitifications",
            content: function () {
                //return Html 
                var compiled = _.template($("#notification-template").html());

                return compiled({ notifications: date })
            },
            placement: "bottom"

        }).on("shown.bs.popover", function () {

            $.post("/api/Notifications/ReadNotifications")
                .done(function () {
                    //botstrap badge set it empty
                    $(".js-notification-count")
                        .text("")
                        .addClass("hide");

                });
            setTimeout(function () {
                //Remove Backgroung-color of new Notification 
                $(".single-notify").removeClass("well");
            }, 5000)
        });

    });
})();