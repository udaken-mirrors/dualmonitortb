﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using DualMonitor.Entities;

namespace DualMonitor.Rules
{
    public class RuleManager
    {
        private readonly string _userDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName, "IconsCache");
        private readonly List<Rule> _rules;
        private readonly WindowManager _windowManager;

        public RuleManager(List<Rule> rules, WindowManager windowManager) 
        {
            _rules = rules;
            _windowManager = windowManager;
        }

        internal void RemoveRule(Rule rule)
        {
            int index = _rules.FindIndex(r => RuleComparer(r, rule));

            if (index >= 0)
            {
                _rules.RemoveAt(index);
            }
        }

        private bool RuleComparer(Rule r1, Rule r2)
        {
            return r1.Id == r2.Id;
        }

        internal void AddRule(Rule r1)
        {
            _rules.Add(r1);
        }

        internal IEnumerable<Rule> GetRules()
        {
            return _rules;
        }

        internal string SaveIcon(Icon icon)
        {
            Bitmap bmp = icon.ToBitmap();
            if (!Directory.Exists(_userDataPath)) Directory.CreateDirectory(_userDataPath);

            string filename = _userDataPath + "\\" + Guid.NewGuid().ToString() + ".png";
            if (!SaveJpeg(filename, bmp, 100))
            {
                return null;
            }

            return filename;
        }

        private bool SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Png);

            if (jpegCodec == null)
                return false;

            EncoderParameters encoderParams = new EncoderParameters(1) {Param = {[0] = qualityParam}};

            img.Save(path, jpegCodec, encoderParams);

            return true;
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            return encoders.FirstOrDefault(t => t.FormatID == format.Guid);
        }

        internal void MatchRule(Win32Window window)
        {
            string program = window.Path;
            string className = window.ClassName;
            string caption = window.Title;

            if (_rules != null)
            {
                foreach (var rule in _rules)
                {
                    if (rule.UseClass && className.IndexOf(rule.Class, StringComparison.CurrentCultureIgnoreCase) < 0)
                    {
                        continue;
                    }

                    if (rule.UseCaption && caption.IndexOf(rule.Caption, StringComparison.CurrentCultureIgnoreCase) < 0)
                    {
                        continue;
                    }

                    if (rule.UseProgram && 
                        (program == null || rule.Program == null || string.Compare(
                            Path.GetFullPath(program).TrimEnd('\\'), 
                            Path.GetFullPath(rule.Program).TrimEnd('\\'), 
                            StringComparison.InvariantCultureIgnoreCase) != 0))
                    {
                        continue;
                    }

                    BaseRuleAction action = 
                        RuleActionFactory.CreateAction(RuleActionType.Move, rule.MoveAction, window, _windowManager);

                    action?.Handle();
                }
            }
        }

        internal void Init()
        {
            var di = new DirectoryInfo(_userDataPath);
            if (!di.Exists) return;

            foreach (var item in di.GetFiles("*.png"))
            {
                if (!_rules.Any(r => r.Icon != null && r.Icon.Equals(item.FullName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    try
                    {
                        item.Delete();
                    }
                    catch
                    {
                        // what to do, what to do ?
                    }
                }
            }
        }

        internal static List<Rule> Clone(IEnumerable<Rule> list)
        {
            // use a copy of the existing rules and work with this list            
            return list.Select(rule => rule.Clone()).ToList();
        }
    }
}
