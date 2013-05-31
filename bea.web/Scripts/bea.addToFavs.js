$(function () {
    $('#bookmarkme').click(function () {
        if (window.sidebar && window.sidebar.addPanel) { // Mozilla Firefox Bookmark
            //window.sidebar.addPanel(document.title,window.location.href,''); -- do nothing
        } else if(window.external && window.external.AddFavorite) { // IE Favorite
            window.external.AddFavorite(location.href,document.title); 
        } else if(window.opera && window.print) { // Opera Hotlist
            this.title = document.title;
            return true;
        } else { // webkit - safari/chrome
            alert('Votre naviguateur ne nous permet pas d\'ajouter cette page pour vous ! \n\rMais vous pouvez l\'ajouter vous même en utilisant le raccourci [' + (navigator.userAgent.toLowerCase().indexOf('mac') != -1 ? 'Command/Cmd' : 'CTRL') + ' + D] .');
        }
    });
});