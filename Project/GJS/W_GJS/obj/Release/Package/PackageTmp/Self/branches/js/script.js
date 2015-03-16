
    function getgooglegmap(LONGITUDE, LATITUDE) {
        var mapOptions = {
            center: new google.maps.LatLng(LATITUDE, LONGITUDE),
            zoom: 10,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"),
          mapOptions);
        // create a marker  
        var latlng = new google.maps.LatLng(LATITUDE, LONGITUDE);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            title: 'My Place'
        });
        infoWindow = new google.maps.InfoWindow({
            content: ""
        });
        infoWindow.open(map, marker);
        geocoder = new google.maps.Geocoder();

        //Update postal address when the marker is dragged  
        google.maps.event.addListener(marker, 'click', function () { //dragend  

            geocoder.geocode({ latLng: marker.getPosition() }, function (responses) {
                if (responses && responses.length > 0) {
                    infoWindow.setContent(
                    "<div style=\"font-size:smaller;\">" + responses[0].formatted_address
                    + "<br />"
                    + "Latitude: " + marker.getPosition().lat() + "&nbsp"
                    + "Longitude: " + marker.getPosition().lng() + "</div>"
                    );
                    infoWindow.open(map, marker);
                } else {
                    alert('Error: Google Maps could not determine the address of this location.');
                }
            });
            map.panTo(marker.getPosition());
        });
        // Close the marker window when being dragged  
        google.maps.event.addListener(marker, 'dragstart', function () {
            infoWindow.close(map, marker);
        });
    }


    function GetDCity() {
        $("#spansearchD").text($("#Dcity option:selected").text());
        if ($("#Dcity").val() != "") {
            $.ajax({
                url: '/Home/GetBrancheslByDCityAJAX',

                type: 'POST',

                data: { CITIES_DETAIL_CD: $("#Dcity").val() },

                dataType: 'json',

                success: function (results) {
                    $('#table-branches').empty();
                    $("#table-branches").append("<tbody>" +
                        "<tr>" +
                            "<td class=\"sys1\">Tên chi nhánh </td>" +
                            "<td class=\"sys2\">Địa chỉ</td>" +
                            "<td class=\"sys3\">Điện thoại</td>" +
                        "</tr>");

                    for (var i = 0; i < results.length; i++) {
                        if (i % 2 == 0) {
                            $("#table-branches").append("<tr class=\"hand-cursor\" onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                        "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                        "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                        "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                    "</tr>");
                        }
                        else {
                            $("#table-branches").append("<tr class=\"even hand-cursor\" onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                        "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                        "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                        "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                    "</tr>");
                        }

                    }
                    $("#table-branches").append("</tbody>");
                },
                error: function (results) {
                    alert('checkloi');
                }


            });
        }
        else {
            $.ajax({
                url: '/Home/GetBrancheslByCityAJAX',

                type: 'POST',

                data: { CITIES_CD: $("#city").val() },

                dataType: 'json',

                success: function (results) {
                    $('#table-branches').empty();
                    $("#table-branches").append("<tbody>" +
                        "<tr>" +
                            "<td class=\"sys1\">Tên chi nhánh </td>" +
                            "<td class=\"sys2\">Địa chỉ</td>" +
                            "<td class=\"sys3\">Điện thoại</td>" +
                        "</tr>");

                    for (var i = 0; i < results.length; i++) {
                        if (i % 2 == 0) {
                            $("#table-branches").append("<tr class=\"hand-cursor\"  onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                        "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                        "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                        "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                    "</tr>");
                        }
                        else {
                            $("#table-branches").append("<tr class=\"even hand-cursor\"  onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                        "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                        "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                        "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                    "</tr>");
                        }

                    }
                    $("#table-branches").append("</tbody>");
                },
                error: function (results) {
                    alert('checkloi');
                }


            });
        }

    }
    function GetCity() {

        $("#spansearch").text($("#city option:selected").text());

        $.ajax({
            url: '/Home/GetDetailByCityAJAX',

            type: 'POST',

            data: { CITIES_CD: $("#city").val() },

            dataType: 'json',

            success: function (results) {
                $('#Dcity').empty();
                $("#Dcity").append("<option value=\"\">---</option>");
                $("#spansearchD").text("---");
                for (var i = 0; i < results.length; i++) {
                    $("#Dcity").append("<option value=\"" + results[i].CITIES_DETAIL_CD + "\">" + results[i].CITIES_DETAIL_NAME + "</option>");

                }
            },
            error: function (results) {
                alert('checkloi');
            }


        });

        $.ajax({
            url: '/Home/GetBrancheslByCityAJAX',

            type: 'POST',

            data: { CITIES_CD: $("#city").val() },

            dataType: 'json',

            success: function (results) {
                $('#table-branches').empty();
                $("#table-branches").append("<tbody>" +
                    "<tr>" +
                        "<td class=\"sys1\">Tên chi nhánh </td>" +
                        "<td class=\"sys2\">Địa chỉ</td>" +
                        "<td class=\"sys3\">Điện thoại</td>" +
                    "</tr>");

                for (var i = 0; i < results.length; i++) {
                    if (i % 2 == 0) {
                        $("#table-branches").append("<tr class=\"hand-cursor\" onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                    "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                    "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                    "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                "</tr>");
                    }
                    else {
                        $("#table-branches").append("<tr class=\"even hand-cursor\" onclick='window.open(\""+ results[i].BRANCH_CODE + "\", \" mywindow\", \"status=1\" );'>" +
                                    "<td class=\"sys1\">" + results[i].BRANCH_NAME + "</td>" +
                                    "<td class=\"sys2\">" + results[i].ADDRESS + "</td>" +
                                    "<td class=\"sys3\">" + results[i].PHONE + "</td>" +
                                "</tr>");
                    }

                }
                $("#table-branches").append("</tbody>");
            },
            error: function (results) {
                alert('checkloi');
            }


        });
    }
