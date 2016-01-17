﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreApplication.Models;
using StoreApplication.Entities;

namespace StoreApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Index page.
        /// </summary>
        /// <returns>Home page.</returns>
        public ActionResult Index()
        {
            StoreAppDBEntities DataBase = new StoreAppDBEntities();
            List<Item> entityList = DataBase.Item.ToList();
            List<ItemModel> modelList = new List<ItemModel>();

            foreach (var item in entityList)
            {
                ItemModel newItem = new ItemModel();
                newItem.Id = item.Id;
                newItem.Name = item.Name;
                newItem.Description = item.Description;
                newItem.Category = item.ItemCategory.Names;
                newItem.Price = item.Price;

                modelList.Add(newItem);
            }

            return View(modelList);
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Index page with list.</returns>
        public ActionResult Delete(Guid id)
        {
            StoreAppDBEntities Database = new StoreAppDBEntities();
            var selectedItem = Database.Item.ToList().Find(item => item.Id == id);
            Database.Item.Remove(selectedItem);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Cancel request.
        /// </summary>
        /// <returns>Home page.</returns>
        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Back to home page.
        /// </summary>
        /// <returns>Home page.</returns>
        public ActionResult Back()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// New item to list.
        /// </summary>
        /// <returns>New item page.</returns>
        public ActionResult NewItem()
        {
            StoreAppDBEntities Database = new StoreAppDBEntities();
            ViewBag.Categories = new SelectList(Database.ItemCategory, "Id", "Names");

            return View(new StoreApplication.Models.ItemModel());
        }

        /// <summary>
        /// Save new item.
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>List of items.</returns>
        [HttpPost]
        public ActionResult NewItem(ItemModel item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                StoreAppDBEntities Database = new StoreAppDBEntities();
                Item entity = new Item();

                entity.Id = item.Id;
                entity.Name = item.Name;
                entity.Description = item.Description;
                entity.Price = item.Price;
                entity.CategoryId = item.CategoryId;
                Database.Item.Add(entity);
                Database.SaveChanges();
                
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        /// <summary>
        /// Item details.
        /// </summary>
        /// <returns>Item details view.</returns>
        public ActionResult ItemDetails(Guid id)
        {
            StoreAppDBEntities Database = new StoreAppDBEntities();
            var selectedItem = Database.Item.ToList().Find(item => item.Id == id);

            ItemModel itemModel = new ItemModel();
            itemModel.Id = selectedItem.Id;
            itemModel.Description = selectedItem.Description;
            itemModel.Name = selectedItem.Name;
            itemModel.Category = selectedItem.ItemCategory.Names;
            itemModel.Price = selectedItem.Price;

            return View(itemModel);
        }

        /// <summary>
        /// More details.
        /// </summary>
        /// <returns>List of items view.</returns>
        public ActionResult MoreDetails()
        {
            StoreAppDBEntities Database = new StoreAppDBEntities();

            List<ItemModel> modelList = new List<ItemModel>();
            List<Item> entityList = Database.Item.ToList();

            foreach (var item in entityList)
            {
                ItemModel modelItem = new ItemModel();
                modelItem.Id = item.Id;
                modelItem.Name = item.Name;
                modelItem.Description = item.Description;
                modelItem.Category = item.ItemCategory.Names;
                modelItem.Price = item.Price;

                Database.SaveChanges();

                modelList.Add(modelItem);
            }

            return View(modelList);
        }
    }
}