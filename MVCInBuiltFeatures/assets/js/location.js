"use strict";

var shopLocation = (function () {

    function distance(lat1, lon1, lat2, lon2, unit) {
        if ((lat1 == lat2) && (lon1 == lon2)) {
            return 0;
        }
        else {
            var radlat1 = Math.PI * lat1 / 180;
            var radlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var radtheta = Math.PI * theta / 180;
            var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
            if (dist > 1) {
                dist = 1;
            }
            dist = Math.acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            if (unit == "K") { dist = dist * 1.609344 }
            if (unit == "N") { dist = dist * 0.8684 }
            return dist;
        }
    }

    var getPositionForAddress = function (address, success) {
        $.ajax({
            dataType: "json",
            url: "https://nominatim.openstreetmap.org/search?q=" + encodeURIComponent(address) + "&format=json",
            success: function (data, textStatus, jqXHR) {
                if (data.length > 0) {
                    var coords = {
                        latitude: data[0].lat,
                        longitude: data[0].lon
                    };
                    success(coords);
                }
            }
        });
    };

    var updateTable = function (position) {
        $(".js-directions").each(function (index, element) {
            var $element = $(element);
            var toAddress = $element.data("toAddress");

            getPositionForAddress(toAddress, function (coords) {
                var d = distance(position.coords.latitude, position.coords.longitude, coords.latitude, coords.longitude, "K");
                $element.find(".js-distance").text(Math.round(d) + "Km");

                $element.attr("href", "https://www.google.com/maps/dir/?api=1&destination=" + encodeURIComponent(toAddress) + "&travelmode=driving&dir_action=navigate");
            });
        });
    };

    var init = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(updateTable);
        } else {
            console.log("Geolocation is not supported by this browser.");
        }
    };

    init();

    return {
        increment: function () {
            changeBy(1);
        }
    };
})();

