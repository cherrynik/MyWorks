const
  EXCEL = document.getElementsByClassName('field__excel');
  FIELD = document.createElement('div');

let
  x = 1,
  y = 8,
  isRealStep = [];

(function() {
  document.body.appendChild(FIELD);
  FIELD.classList.add('field');
})();

// Создание шахматного поля
function createArena() {
  let newPoint = document.createElement('div');

  newPoint.classList.add('field__excel');
  FIELD.appendChild(newPoint);
}

// Раскрашивание полей
function createExcels(count) {
  if (x > 8) {
    x = 1;
    y--;
  }

  if (
    (count % 2 === 0 && y % 2 === 0)
    || (count % 2 !== 0 && y % 2 != 0)
  ) {
    EXCEL[count].classList.add('field__excel_color_first');
  } else {
    EXCEL[count].classList.add('field__excel_color_second');
  }
    
  EXCEL[count].setAttribute('posX', x);
  EXCEL[count].setAttribute('posY', y);
  x++;
}


for (let i = 0; i < 64; i++) {
  createArena();
}

for (let i = 0; i < EXCEL.length; i++) {
  createExcels(i)
};

[].forEach.call(EXCEL, function(current) {
  current.onclick = function() {
    const isKnight    = document.querySelector('.field__excel.field__excel_current');
    const possiblePos = document.querySelectorAll('.field__excel.field__excel_possiblePos');
    if (isKnight && possiblePos) {
      isKnight.classList.remove('field__excel_current');
      [].forEach.call(possiblePos, function(el) {
        el.classList.remove('field__excel_possiblePos')
      });
    }

    let
      clicked   = true,

      currentX  = this.getAttribute('posX'),
      currentY  = this.getAttribute('posY'),

      nextSteps = [
        document.querySelector(`[posX = "${+currentX + 1}"][posY = "${+currentY + 2}"]`),
        document.querySelector(`[posX = "${+currentX + 2}"][posY = "${+currentY + 1}"]`),
        document.querySelector(`[posX = "${+currentX + 2}"][posY = "${+currentY - 1}"]`),
        document.querySelector(`[posX = "${+currentX + 1}"][posY = "${+currentY - 2}"]`),
        document.querySelector(`[posX = "${+currentX - 1}"][posY = "${+currentY - 2}"]`),
        document.querySelector(`[posX = "${+currentX - 2}"][posY = "${+currentY - 1}"]`),
        document.querySelector(`[posX = "${+currentX - 2}"][posY = "${+currentY + 1}"]`),
        document.querySelector(`[posX = "${+currentX - 1}"][posY = "${+currentY + 2}"]`),
      ];
    
    if (clicked) {
      

      clicked = false;
      lastClicked = this;

      this.classList.add('field__excel_current');

      if (isRealStep.length) {
        isRealStep = [];

        for (let i = nextSteps.length - 1; i >= 0; i--) {
          !nextSteps[i] ? nextSteps.splice(i, 1) : isRealStep.push(nextSteps[i]);
        }
      } else {
        for (let i = nextSteps.length - 1; i >= 0; i--) {
          !nextSteps[i] ? nextSteps.splice(i, 1) : isRealStep.push(nextSteps[i]);
        }
      }
    }
      
    isRealStep.forEach(function(el) {
      el.classList.add('field__excel_possiblePos');
    });
  };
});
