$( document ).ready(function() {
    $('.delete-note').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/DeleteTweet',
            data: {
                ID: NoteID
            }
        });
    })

    $('.definitely-delete').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/DefinitelyDelete',
            data: {
                ID: NoteID
            }
        });
    });

    $('.restore-note').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/RestoreNote',
            data: {
                ID: NoteID
            }
        });
    });
});

