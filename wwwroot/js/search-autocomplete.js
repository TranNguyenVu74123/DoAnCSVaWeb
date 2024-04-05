$(document).ready(function () {
    $('#searchInput').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '@Url.Action("Search", "Home")',
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
        minLength: 1,
        appendTo: ".autocomplete-suggestions",
        create: function () {
            $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                return $('<li>')
                    .append('<div>' + item + '</div>')
                    .appendTo(ul);
            };
        }
    });

    $('#searchForm').submit(function (e) {
        e.preventDefault();
        var searchTerm = $('#searchInput').val();
        window.location.href = '/Home/Search?query=' + searchTerm;
    });
});
