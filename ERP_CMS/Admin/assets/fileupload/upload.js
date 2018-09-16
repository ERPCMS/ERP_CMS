$(function () {

    $('.btnFileUpload').fileupload({
        url: 'IRHandler.ashx?action=upload',
        add: function (e, data) {
            console.log('add', data);
            $('#progressbar').show();
            data.submit();
        },
        progress: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progressbar div').css('width', progress + '%');
        },
        success: function (response, status) {
            $('#progressbar').hide();
            $('#progressbar div').css('width', '0%');
            var resp = response.split('||');
            $('#hdnDoc').val(resp[1]);
            $('#lnkAttachment').css('cursor', 'pointer');
            $('.divImgDoc').show();
            console.log('success', response);
        },
        error: function (error) {
            $('#progressbar').hide();
            $('#progressbar div').css('width', '0%');
            console.log('error', error);
        }
    });
});