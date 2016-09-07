using System.Collections.Generic;
using DualMonitor.Controls;
using System.Drawing;
using System.Linq;

namespace DualMonitor.Entities
{
    public class ProcessGroupManager
    {
        private readonly Dictionary<string, ProcessGroup> _groups = new Dictionary<string, ProcessGroup>();
        private readonly TaskbarFlow _flow;

        public ProcessGroupManager(TaskbarFlow flow)
        {
            _flow = flow;
        }

        public ProcessGroup GetButtonsByPath(string path)
        {
            ProcessGroup group;
            if (!_groups.TryGetValue(path, out group))
            {
                group = new ProcessGroup();
                _groups.Add(path, group);
            }

            return group;
        }

        public void AddToGroup(string path, BaseTaskbarButton button)
        {
            path = path.ToLower().Trim();

            // get or create new group for this application
            ProcessGroup group;
            if (!_groups.TryGetValue(path, out group))
            {
                group = new ProcessGroup();
                _groups.Add(path, group);
            }

            if (group.Count == 0)
            {
                // if only item in this group, add it to the end of the taskbar
                group.Add(button);
                _flow.Controls.SetChildIndex(button, _flow.Controls.Count);
            }
            else
            {                
                int index = group.GetFirstIndex(_flow.Controls);
                group.Add(button);
                group.Arrange(_flow.Controls, index);

                TaskbarPinnedButton pinnedButton = group.GetPinnedButton();
                if (pinnedButton != null)
                {
                    pinnedButton.Visible = false;
                }
            }            
        }

        public void RemoveFromGroup(string path, BaseTaskbarButton button)
        {
            path = path.ToLower().Trim();

            List<BaseTaskbarButton> list = GetButtonsByPath(path);

            list.Remove(button);

            if (list.Count == 1)
            {
                if (list[0] is TaskbarPinnedButton) list[0].Visible = true;
            }
        }

        public bool MoveButtonToPoint(Point clientCoords, BaseTaskbarButton button)
        {
            var moved = false;
            var draggedGroup = _groups.Values.FirstOrDefault(item => item.Contains(button));

            if (draggedGroup == null) return false; 

            foreach (var path in _groups.Keys)
            {
                var group = _groups[path];

                if (group == draggedGroup || group.Count == 0) continue;

                var bounds = group.GetBounds();
                var index = -1;

                var taskbarLocation = _flow.MainForm.TaskbarLocation;

                if (taskbarLocation == Win32.Native.ABEdge.Top
                    || taskbarLocation == Win32.Native.ABEdge.Bottom)
                {
                    int middle = bounds.Left + bounds.Width / 2;                    

                    if (clientCoords.X <= middle - 10 && clientCoords.X >= bounds.Left)
                    {
                        index = group.GetFirstIndex(_flow.Controls);
                    }
                    else if (clientCoords.X >= middle + 10 && clientCoords.X <= bounds.Right)
                    {
                        index = group.GetLastIndex(_flow.Controls) + 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    int middle = bounds.Top + bounds.Height / 2;

                    if (clientCoords.Y <= middle - 10 && clientCoords.Y >= bounds.Top)
                    {
                        index = group.GetFirstIndex(_flow.Controls);
                    }
                    else if (clientCoords.Y >= middle + 10 && clientCoords.Y <= bounds.Bottom)
                    {
                        index = group.GetLastIndex(_flow.Controls) + 1;
                    }
                    else
                    {
                        continue;
                    }
                }

                draggedGroup.Arrange(_flow.Controls, index);

                moved = true;
            }

            return moved;
        }
    }
}
