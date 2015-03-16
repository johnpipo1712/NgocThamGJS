


function CheckNew(NEWS_CD, control) {

    $.ajax({
        url: '/News/CheckNewAjax',

        type: 'POST',

        data: { NEWS_CD: NEWS_CD, CHECK: control.checked },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Lỗi');
        }


    });

}
function CheckHightlight(NEWS_CD, control) {

    $.ajax({
        url: '/News/CheckHightlightAjax',

        type: 'POST',

        data: { NEWS_CD: NEWS_CD, CHECK: control.checked },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Lỗi');
        }


    });

}
