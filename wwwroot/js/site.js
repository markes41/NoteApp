$( document ).ready(function() {
    $('.delete-note').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/ToDeleteNote',
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

    $('.archieve-note').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/ToArchiveNote',
            data: {
                ID: NoteID
            }
        });
    });

    $('.new-note-title').hide();

    $('.new-note-body').on('click', function(){
      $(this).prev('.new-note-title').show();
    });

    $('.fa-times').on('click', function(){
      $('.new-note-title').hide();
      $('.new-note-body').val("");
    });

    $('.fa-plus').on('click', function(){
        var titulo = $('.new-note-title').val();
        var cuerpo = $('.new-note-body').val();
        $.ajax({
            type: 'POST',
            url: '/Note/ToCreateNote',
            data: {
                title: titulo,
                body: cuerpo
            }
        });
    });

    $('.restore-note').on('click', function(){
        var NoteID = $(this).next('.NoteID').val();
        $('.body-'+ NoteID).hide();

        $.ajax({
            type: 'POST',
            url: '/Note/UnarchiveNote',
            data: {
                ID: NoteID,
            }
        });
    });
});

