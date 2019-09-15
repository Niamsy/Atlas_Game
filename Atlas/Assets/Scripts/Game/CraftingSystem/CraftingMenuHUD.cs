using System;
using AtlasAudio;
using AtlasEvents;
using Game;
using Game.Crafting;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Menu.Crafting;
using Menu.Inventory.ItemDescription;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftingMenuHUD : Menu.MenuWidget
{
    [Header("Crafting")] 
    [SerializeField] private GridProducingHUD onGoingProducingHud = null;
    [SerializeField] private GridProducingHUD finishedProducingHud = null;
    
    [Header("Production button")]
    [SerializeField] private Button produceButton = null;
    [SerializeField] private Color enabledButton = Color.green;
    [SerializeField] private Color disabledButton = Color.gray;
    
    [Header("Audio")] 
    public Audio onToggleGuiAudio = null;
    public AudioEvent onToggleGuiEvent = null;

    [Header("Descriptions")] 
    [SerializeField] private GameObject recipeDescription = null;
    [SerializeField] private GameObject ingredientDescription = null;

    private RecipeDescriptionHUD _description;
    private IngredientDescriptionSimpleHUD _ingredientDesc;
    private GridCraftingHUD _crafting;
    private Crafter _crafter;
    private Recipe _selectedRecipe;

    private void OnEnable()
    {
        if (recipeDescription)
        {
            _description = recipeDescription.GetComponent<RecipeDescriptionHUD>();
        }

        if (ingredientDescription)
        {
            _ingredientDesc = ingredientDescription.GetComponent<IngredientDescriptionSimpleHUD>();
        }

        _crafting = GetComponentInChildren<GridCraftingHUD>();
        if (onGoingProducingHud == null)
        {
            Debug.LogWarning("On going production list not set on Crafting Menu HUD");
        }
        else
        {
            onGoingProducingHud.SetClickCb(OnProductionCancel);
        }

        if (finishedProducingHud == null)
        {
            Debug.LogWarning("Finished production list not set on Crafting Menu HUD");
        }
        else
        {
            finishedProducingHud.SetClickCb(OnProductFinishedRetrieve);
        }

        if (produceButton)
        {
            produceButton.onClick.AddListener(Produce);
        }
    }

    private void OnDisable()
    {
        SaveManager.Instance.InputControls.Player.Interact.performed -= OpenCloseCraftingMenu;
    }

    private void OpenCloseCraftingMenu(InputAction.CallbackContext obj)
    {
        Show(!Displayed);
        if (onToggleGuiAudio && onToggleGuiEvent)
        {
            onToggleGuiEvent.Raise(onToggleGuiAudio, null);
        }
    }

    public void Produce()
    {
        if (_selectedRecipe && _crafter.CanProduce(_selectedRecipe, _crafter.Inventory))
        {
            _crafter.Produce(_selectedRecipe, _crafter.Inventory);
            onGoingProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsOngoing);
            OnRecipeSelected(_selectedRecipe);
        }
    }

    
    public void OnRecipeSelected(Recipe recipe)
    {
        var canProduce = recipe && recipe.isUnlocked && _crafter.CanProduce(recipe, _crafter.Inventory);
        
        _selectedRecipe = recipe;
        produceButton.image.color = canProduce ? enabledButton : disabledButton;
        produceButton.enabled = canProduce;
        Debug.Log("Recipe selected " + recipe.name + ", can produce " + canProduce);
    }
    
    public void SetCrafter(Crafter crafter)
    {
        _crafter = crafter;
        _crafting.LoadThisBook(crafter.RecipeBook, OnRecipeSelected);
        _crafter.SetOnProductFinishedCb(OnProductFinished);
        onGoingProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsOngoing);
        finishedProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsFinished);
    }

    public void UnsetCrafter(Crafter crafter)
    {
        _crafter.SetOnProductFinishedCb(null);
        _crafter = null;
        _crafting.Unload();
    }

    public void OnProductFinished(Recipe.Product product, int position)
    {
        onGoingProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsOngoing);
        finishedProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsFinished);
    }

    public void OnProductionCancel(Recipe.Product product, int position)
    {
        _crafter.CancelProduction(position);
        onGoingProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsOngoing);
    }

    public void OnProductFinishedRetrieve(Recipe.Product product, int position)
    {
        var prod = _crafter.GetFinishedProduct(position); // we are forced to call this method, think of some encapsulation
        finishedProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsFinished);

        var itemStack = new ItemStack();
        itemStack.SetItem(prod.Item, product.ProducedQuantity);

        _crafter.Inventory.AddItemStack(itemStack);
        
    }

    public override void Show(bool display, bool force = false)
    {
        base.Show(display, force);
        if (_description) _description.Reset();
        if (_ingredientDesc) _ingredientDesc.Reset();
        if (display)
        {
            SaveManager.Instance.InputControls.Player.Interact.performed += OpenCloseCraftingMenu;
            if (onGoingProducingHud) 
                onGoingProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsOngoing);
            if (finishedProducingHud)
                finishedProducingHud.SetProducts(_selectedRecipe, _crafter.ProductsFinished);
        }
        else
        {
            SaveManager.Instance.InputControls.Player.Interact.performed -= OpenCloseCraftingMenu;
        }
    }
    
    protected override void InitialiseWidget()
    {
    }
}
