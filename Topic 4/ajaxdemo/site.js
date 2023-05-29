$(function () {

    console.log("jQuery is ready");

    $("#add-song").click(function () {
        var song = {
            title: $('#title').val(),
                artist: $('#artist').val()
    };

    $.ajax({
        type: 'GET',
        url: 'putsong.php',
        dataType: 'json',
        data: song,
        success: function (newsong) {
            console.log("Here is the song the server sent back to us:");
            console.log(newsong);
            var songstring = '<li>Title:  ' + newsong.title + '  Artist: ' + newsong.artist + '</li>';
            $(songstring).appendTo('#songs').hide().fadeIn();
        }
    })
});

    $("#get-songs-from-api").click(function () {
        console.log("Get songs button was clicked");

        $.ajax({
            dataType: "json",
            url: "getsongs.php",
            success: function (songs) {
                console.log("Here is the list of songs I got from the server : ");
                console.log(songs);

                $.each(songs, function (i, song) {
                    var songstring = '<li>Title:  ' + song.title + '  Artist: ' + song.artist + '</li>';
                    $(songstring).appendTo('#songs').hide().fadeIn();
                })
            }
        });
    });
});