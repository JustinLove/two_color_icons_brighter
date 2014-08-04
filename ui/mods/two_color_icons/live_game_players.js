(function() {
  $.get('coui://ui/mods/ui_icon_secondary_color/player-outline-mask.html', function(html) {
    $('svg:first').after(html)
  });

  var brighter = function(n) {
    return Math.floor(n + (255-n)*0.2)
  }

  model.secondary = function(army) {
    return 'rgb(' + brighter(army.secondary_color[0]) + ',' + brighter(army.secondary_color[1]) + ',' + brighter(army.secondary_color[2]) + ')';
  }

  $('.div_player_icon .outline').replaceWith('<div class="outline outline_masked" data-bind="style: {backgroundColor: $parent.secondary($data)}"></div>')
})()
