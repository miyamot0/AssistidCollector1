﻿//----------------------------------------------------------------------------------------------
// <copyright file="CardCheckTemplate.cs" 
// Copyright February 2, 2018 Shawn Gilroy
//
// This file is part of AssistidCollector2
//
// AssistidCollector2 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// AssistidCollector2 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AssistidCollector2.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
//
// <summary>
// The AssistidCollector2 is a tool to assist clinicans and researchers in the treatment of communication disorders.
// 
// Email: shawn(dot)gilroy(at)temple.edu
//
// </summary>
//
// =============================================================================================
//
// This class is derived from existing methods and styles from Adam Wolf at 
// https://github.com/awolf/Xamarin-Forms-InAnger. The base methods have been
// adjusted to improve the clarity, simplicity, and recognizability of
// information presented to users. The license for this work is indicated below:
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//----------------------------------------------------------------------------------------------

using AssistidCollector1.Enums;
using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class CardCheckTemplate : ContentView
    {
        public Identifiers.Pages PageId = Identifiers.Pages.LateOnset;
        public Grid grid;

        public CardCheckTemplate(string title, string instructions, Identifiers.Strategies strategyType)
        {
            grid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (70, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (70, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) }
                }
            };

            grid.Children.Add(new CardStatusView(strategyType), 0, 1, 0, 2);

            grid.Children.Add(new Switch
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            }, 4, 5, 0, 1);

            grid.Children.Add(new CardDetailsView(title, instructions) 
            {
                Margin = new Thickness(0, 5, 0, 5)
            }, 1, 4, 0, 1);

            grid.Children.Add(new CardButtonView("Please check the box if completed."), 1, 5, 1, 2);

            grid.Margin = new Thickness(0, 0, 0, 10);

            Content = grid;
        }
    }
}