
function CheckNew(PRODUCT_CD,control) {
  
        $.ajax({
            url: '/Product/CheckNewAjax',

            type: 'POST',

            data: { PRODUCT_CD: PRODUCT_CD, CHECK: control.checked },

            dataType: 'json',

            success: function (results) {
                        
                },
            error: function (results) {
                alert('Loi');
            }


        });

    }
function CheckHightlight(PRODUCT_CD, control) {

    $.ajax({
        url: '/Product/CheckHightlightAjax',

        type: 'POST',

        data: { PRODUCT_CD: PRODUCT_CD, CHECK: control.checked },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Loi');
        }


    });

}

function CheckSelling(PRODUCT_CD, control) {

    $.ajax({
        url: '/Product/CheckSellingAjax',

        type: 'POST',

        data: { PRODUCT_CD: PRODUCT_CD, CHECK: control.checked },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Loi');
        }


    });

}


function CheckDisplay(PRODUCT_CD, control) {

    $.ajax({
        url: '/Product/CheckDisplayAjax',

        type: 'POST',

        data: { PRODUCT_CD: PRODUCT_CD, CHECK: control.checked },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Loi');
        }


    });

}
