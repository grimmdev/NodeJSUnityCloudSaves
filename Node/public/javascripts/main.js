$.get("cloud/count", function (data) {
	if(data.count == 1)
		$("h2").html(data.count + " Save");
	else
		$("h2").html(data.count + " Saves");
});

$.get("cloud/all", function (data) {
	$.each(data, function (index, value) {
		$("tbody").append('<tr><th scope="row">' + index + '</th><th scope="row">' + value.id + '</th><th scope="row">' + value.data + '</th>');
	});
});