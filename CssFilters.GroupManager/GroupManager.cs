﻿using System.Reflection;
using CounterStrikeSharp.API.Core;
using CssFilters.CommandManager.Models;
using CssFilters.GroupManager.Attributes;
using CssFilters.GroupManager.Models;
using CssFilters.GroupManager.Options;
using CssFilters.Interfaces;
using CssFilters.Utilities;

namespace CssFilters.GroupManager
{
	public class GroupManager : IFilterManager
	{
		#region Data
		private BasePlugin _plugin => Options.Plugin;
		private FilterManager _filterManager;
		private FilterLogger _filterLogger => Options.FilterLogger;

		internal HashSet<GroupCommandsModel> GroupCommandsModels;
		#endregion

		#region Properties
		private GroupManagerOptions options = new();
		public GroupManagerOptions Options
		{
			get
			{
				return options;
			}
		}
		#endregion

		#region .ctor
		public GroupManager(FilterManager filterManager)
		{
			GroupCommandsModels = new HashSet<GroupCommandsModel>();
			_filterManager = filterManager;
		}
		#endregion

		#region Public
		/// <summary>
		/// Добавление группы для фильтров.
		/// </summary>
		/// <param name="name">Название группы.</param>
		/// <param name="filterCommands">Фильтр.</param>
		/// <returns></returns>
		public GroupManager AddGroupForCommands(string name, List<FilterCommandBase> filterCommands)
		{
			GroupCommandsModels.Add(new GroupCommandsModel(name, filterCommands));
			return this;
		}

		/// <summary>
		/// Добавляет команду в группы.
		/// </summary>
		/// <param name="filterCommand">Фильтр для команды.</param>
		/// <param name="groupName">Название группы.</param>
		/// <returns></returns>
		public GroupManager AddCommandInGroups(FilterCommandBase filterCommand, params string[] groupNames)
		{
			var groupCommandsModels = GroupCommandsModels.Where(x => groupNames.Contains(x.Name));

			if(groupCommandsModels is null)
			{
				throw new ArgumentNullException("При добавлении команды в группы ни одна из групп не найдена.");
			}

			foreach (var groupCommandsModel in groupCommandsModels)
			{
				groupCommandsModel.CommandFilters.Add(filterCommand);
			}
			return this;
		}

		/// <summary>
		/// Изменяет настройки менеджера групп.
		/// </summary>
		/// <param name="options">Настройки.</param>
		/// <returns></returns>
		public GroupManager ChangeOptions(Action<GroupManagerOptions> options)
		{
			options(Options);
			return this;
		}
		#endregion

		#region Internal
		internal void AutoSearchGroups()
		{
			var pluginAssembly = Options.PluginAssembly;
			var typesWithAttributeGroupName = pluginAssembly.GetTypes()
				.Where(t => t.GetCustomAttributes<GroupNameAttribute>(false).Any() &&
				!t.IsAbstract);

			string[] existingGroups = [];
			foreach (var type in typesWithAttributeGroupName)
			{
				var groupNameAttribute = type.GetCustomAttribute<GroupNameAttribute>(false)!;
				var filterCommandInstance = Activator.CreateInstance(type) as FilterCommandBase;
				if (filterCommandInstance == null)
				{
					throw new ArgumentNullException(nameof(filterCommandInstance) + " не наследуется от FilterCommandBase.");
				}

				if (GroupCommandsModels.Where(x => x.Name == groupNameAttribute.GroupName).SingleOrDefault() is null)
				{
					AddGroupForCommands(groupNameAttribute.GroupName, new List<FilterCommandBase>
					{
						filterCommandInstance
					});
				}
				else
				{
					AddCommandInGroups(filterCommandInstance, groupNameAttribute.GroupName);
				}
			}
		}
		#endregion
	}
}
