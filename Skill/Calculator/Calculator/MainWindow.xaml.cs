using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{

  public partial class MainWindow
  {


    private double _currentNumber;
    private string _evaluateEx;
    private bool   _isFirstInput = true;
    private bool   _isFloat;
    private bool   _isNewEx;
    private int    _operationCounter;

    public MainWindow() { InitializeComponent(); }

    private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) {
      if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    private void MainWindow_OnMouseRightButtonUp(
      object               sender,
      MouseButtonEventArgs e
    ) {
      Close();
    }

    private void Operation(object sender, RoutedEventArgs e) {
      var content = ((TextBlock) ((Button) sender).Content).Text;

      switch (content) {
        case "AC":
          AllClear();

          break;
        case "±":
          PlusMinus();

          break;
        case "%":
          Percentage();

          break;
        default:
          MakeOperation(Convert.ToChar(content));

          break;
      }
    }

    private void PutNumber(object sender, RoutedEventArgs e) {
      if (_isNewEx) {
        AllClear();
        _isNewEx = false;
      }

      var content = ((TextBlock) ((Button) sender).Content).Text;
      _isFloat = Result.Text.IndexOf('.') != -1;

      if (content != "." &&
          (Result.Text == "0" || _isFirstInput)) {
        Result.Text = content;
      } else if (content == "." &&
                 !_isFloat) {
        _isFloat    =  true;
        Result.Text += content;
      } else if (content != ".") {
        Result.Text += content;
      } else {
        Result.Text = Regex.Replace(
          Result.Text,
          @"(^\-?)0+?[\.](0?)+|[\.]",
          "$1"
        );

        Result.Text += ".";
      }

      _isFirstInput = false;
    }

    private void AllClear() {
      _currentNumber    = 0;
      _operationCounter = 0;
      History.Text      = "";
      Result.Text       = "0";
      _isFirstInput     = true;
      _isFloat          = false;
      _evaluateEx       = string.Empty;
    }

    private void PlusMinus() {
      Result.Text = $"{-Convert.ToDouble(Result.Text)}";
    }

    private void Percentage() {
      Result.Text = $"{Convert.ToDouble(Result.Text) / 100}";
    }

    private void MakeOperation(char ch) {
      if (_isNewEx) {
        AllClear();
        _isNewEx = false;
      }

      _currentNumber =  Convert.ToDouble(Result.Text);
      _evaluateEx    += $"{_currentNumber}";

      if (ch != '=') {
        _operationCounter++;

        History.Text +=
          ch != '—' ? $"{_currentNumber} {ch} " : $"{_currentNumber} - ";

        if (_operationCounter == 2) {
          Result.Text =
            $"{Convert.ToDouble(new DataTable().Compute(_evaluateEx, ""))}";

          _evaluateEx       = Result.Text;
          _operationCounter = 1;
        } else {
          Result.Text = $"{_currentNumber}";
        }

        _evaluateEx +=
          ch == '÷'
            ? "/"
            : ch == '✕'
              ? "*"
              : ch == '—'
                ? "-"
                : "+";
      } else {
        Result.Text =
          $"{Convert.ToDouble(new DataTable().Compute(_evaluateEx, ""))}";

        History.Text = _isNewEx
          ? $"{_currentNumber} = "
          : History.Text + $"{_currentNumber} = ";

        _isNewEx = true;
      }

      _isFirstInput = true;
    }
  }

}