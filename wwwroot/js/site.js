$( document ).ready(function() {
    $('.note-body').on('click', function(){
        var id = $(this).next('#NoteID').val();
        alert(id)
    })
});

