$(document).ready(function () {
    $('.form-control').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/Search', // Sử dụng URL của action Search
                dataType: 'json',
                data: {
                    term: request.term
                },
                success: function (data) {
                    response(data);
                },
                error: function () {
                    response([]);
                }
            });
        },
        minLength: 1
    });
});