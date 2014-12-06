(function() {
  $.get('coui://ui/mods/ui_icon_secondary_color/player-outline-mask.html', function(html) {
    $('svg:first').after(html)
  });

  var brighter = function(c, b) {
    return Math.min(255, Math.max(0, Math.floor(b * c)))
  }

  var brighterRgb = function(color, b) {
    return 'rgb(' + brighter(color[0], b) + ',' + brighter(color[1], b) + ',' + brighter(color[2], b) + ')';
  }

  model.primary = function(army) {
    return brighterRgb(army.primary_color, 1.1)
  }

  model.secondary = function(army) {
    if (army.secondary_color.toString() == army.primary_color.toString()) {
      return 'rgb(0, 0, 0)'
    } else {
      return brighterRgb(army.secondary_color, 1.1)
    }
  }

  $('.div_player_icon .fill').attr('data-bind', "style: {backgroundColor: $root.primary($data)}")

  $('.div_player_icon .outline').replaceWith('<div class="outline outline_masked" data-bind="style: {backgroundColor: $root.secondary($data)}"></div>')
})()
