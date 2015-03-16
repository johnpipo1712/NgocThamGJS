
function changePST(CUSTOMER_CD, control) {
   
     $.ajax({
        url: '/Customer/ChangePstAjax',

        type: 'POST',

        data: { CUSTOMER_CD: CUSTOMER_CD, change: control.options[control.selectedIndex].value },

        dataType: 'json',

        success: function (results) {

        },
        error: function (results) {
            alert('Lỗi');
        }


    });

}


//function CheckPst(CUSTOMER_CD, control) {

//    $.ajax({
//        url: '/Customer/CheckPstAjax',

//        type: 'POST',

//        data: { CUSTOMER_CD: CUSTOMER_CD, CHECK: control.checked },

//        dataType: 'json',

//        success: function (results) {

//        },
//        error: function (results) {
//            alert('Lỗi');
//        }


//    });

//}
