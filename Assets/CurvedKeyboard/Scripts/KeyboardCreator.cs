﻿using System;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;
using HoloLensKeyboard;

//namespace HoloLensKeyboard
//{
    /// <summary>
    /// Initializes the keyboard.
    /// </summary>
    [ExecuteInEditMode]
    public class KeyboardCreator : Singleton<KeyboardCreator>
    {

        public float old_slider_val = 0;
        public float new_slider_val = 0;

        //[Range(0, 1)]
        //[SerializeField]
        //[Tooltip("How much the keyboard is curved (in percent)")]

        [Tooltip("The component responsible for output")]
        public KeyboardOutput Output;

        // defaults
        //------------
        public float SPACING_COLUMM = 56.3f;
        public float SPACING_ROW = 1.0f;
        public float ROTATION = 90f;
        public float RADIUS = 3f;
        public int SPACE_INDEX = 28;
        public float curvature = 0.2f;
        public float KEY_SIZE = 0.1f;

        // private
        //------------
        public KeyboardItem[] keys;
        public ColorTheme theme;
        public SpaceKeyboardItem space;

        public bool isLowerCase = true;
        public bool isSpecial;

        public int row;
        public float distanceToCenter = -1f;

        public String currentTheme = "default";

        #region Public Methods

        /// <summary>
        /// Handles a selected key.
        /// </summary>
        /// <param name="tapped">The key that was tapped.</param>
        public void HandleTap(KeyboardItem tapped)
        {
            string value = tapped.Value;
            if (value.Equals(Keys.QEH) || value.Equals(Keys.ABC))
            {
                SwitchSpecialKeys();
            }
            else if (value.Equals(Keys.UP) || value.Equals(Keys.LOW))
            {
                SwitchUpperAndLowerKeys();
            }
            else if (value.Equals(Keys.SPACE))
            {
                Output.OnKey(Keys.SPACE);
            }
            else if (value.Equals(Keys.BACK))
            {
                Output.OnBack();
            }
            else
            {
                Output.OnKey(value);
            }
        }

        /// <summary>
        /// Calculates the rotation of a single key on a circle.
        /// </summary>
        /// <param name="rowSize"></param>
        /// <param name="offset"></param>
        /// <returns>The rotation in degrees.</returns>
        public float CalculateKeyRotation(float rowSize, float offset)
        {
            return Mathf.Deg2Rad * (ROTATION + rowSize
                * SpacingBetweenKeys / 2 - offset
                * SpacingBetweenKeys);
        }

        public Vector3 CalculatePositionOnCylinder(float degree)
        {
            return new Vector3(
                Mathf.Cos(degree) * distanceToCenter,
                -row * SPACING_ROW,
                Mathf.Sin(degree) * distanceToCenter);
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        public void Initialize()
        {
            if (distanceToCenter == -1f)
            {
                CurvatureToDistance();
            }

            theme = GetColorTheme(currentTheme);
            keys = InitializeKeys();
        }

        public void enableKeyboard(string tag)
        {
            if (distanceToCenter == -1f)
            {
                CurvatureToDistance();
            }

            theme = GetColorTheme(tag);
            keys = InitializeKeys();
        }

        private void OnValidate()
        {
            CurvatureToDistance();
            UpdateKeys();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the theme based on a tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        private ColorTheme GetColorTheme(string tag)
        {
            // search locally
            var colorThemes = GetComponentsInChildren<ColorTheme>();
            Debug.Log(colorThemes.Length);
            var theme = FindColorTheme(colorThemes, tag);

            // search globally
            if (theme == null)
            {
                colorThemes = FindObjectsOfType<ColorTheme>();
                theme = FindColorTheme(colorThemes, tag);
            }

            return theme;
        }

        /// <summary>
        /// Finds a theme based on themes and a tag.
        /// </summary>
        /// <param name="colorThemes">The found themes.</param>
        /// <param name="tag">The expected tag.</param>
        /// <returns>The theme.</returns>
        private ColorTheme FindColorTheme(ColorTheme[] colorThemes, string tag)
        {
            for (int i = 0; i < colorThemes.Length; ++i)
            {
                if (colorThemes[i].Tag == tag)
                {
                    return colorThemes[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all keys for this GameObject and initializes them.
        /// </summary>
        /// <returns>The keys.</returns>
        private KeyboardItem[] InitializeKeys()
        {
            keys = GetComponentsInChildren<KeyboardItem>();
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].Initialize(i, theme, KeySet.LowerCase);
                SetKeyPosition(keys[i]);
                //keys[i].GetComponent<BoxCollider>().enabled = false;
            }
            space = GetComponentInChildren<SpaceKeyboardItem>();
            space.BuildMesh(this, null);

            return keys;
        }

        /// <summary>
        /// Updates the keys.
        /// </summary>
        /// <remarks>
        public void UpdateKeys()
        {
            if (theme == null || keys == null || space == null)
            {
                Initialize();
            }

            for (int i = 0; i < keys.Length; i++)
            {
                SetKeyPosition(keys[i]);
                SetKeySize(keys[i]);
            }

            space.BuildMesh(this, null);
        }

        /// <summary>
        /// Switches between the <see cref="KeySet.UpperCase"/> and <see cref="KeySet.LowerCase"/> set.
        /// </summary>
        private void SwitchUpperAndLowerKeys()
        {
            SetKeys(isLowerCase ? KeySet.UpperCase : KeySet.LowerCase);
            isLowerCase = !isLowerCase;
        }

        /// <summary>
        /// Switches between the <see cref="KeySet.Special "/> and <see cref="KeySet.LowerCase"/> set.
        /// </summary>
        private void SwitchSpecialKeys()
        {
            SetKeys(isSpecial ? KeySet.Special : KeySet.LowerCase);
            isSpecial = !isSpecial;
            isLowerCase = true;
        }

        /// <summary>
        /// Sets the set for all keys.
        /// </summary>
        /// <param name="state"></param>
        private void SetKeys(KeySet state)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].SetKeyText(state);
            }
        }

        private void SetKeyPosition(KeyboardItem key)
        {
            int iteration = key.Index;
            Transform keyTransform = key.transform;
            // Check row and how many keys were placed
            float keysPlaced = CalculateKeyOffsetAndRow(iteration);
            float degree = CalculateKeyRotation(Keys.LettersInRowsCount[row] - 1, iteration - keysPlaced);
            //caluclate position on cylinder with circle equation formula
            //http://www.mathopenref.com/coordparamcircle.html
            key.transform.localPosition = CalculatePositionOnCylinder(degree);
            //rotate keys by their placement angle
            key.transform.localEulerAngles = new Vector3(0, -degree * Mathf.Rad2Deg - 90f, 0);
            // keys are moved from center couse of increasing circle radius,
            // so position must be restored to radius
            key.transform.localPosition = RestorePosition(key);
        }

        private void SetKeySize(KeyboardItem key)
        {
            key.transform.localScale = new Vector3(KEY_SIZE, KEY_SIZE, 0.1f);
        }

        private float CalculateKeyOffsetAndRow(int iteration)
        {
            float keysPlaced = 0;
            row = 0;
            int iterationCounter = 0;
            for (int rowChecked = 0; rowChecked <= 2; rowChecked++)
            {
                iterationCounter += Keys.LettersInRowsCount[rowChecked];
                if (iteration >= iterationCounter)
                {
                    keysPlaced += Keys.LettersInRowsCount[rowChecked];
                    row++;
                }
            }
            //last row with space requires special calculations
            if (iteration >= iterationCounter)
            {
                const float offsetBetweenSpecialKeys = 1.5f;
                keysPlaced -= (iteration - iterationCounter) * offsetBetweenSpecialKeys;
            }
            return keysPlaced;
        }

        private Vector3 RestorePosition(KeyboardItem key)
        {
            return new Vector3(
                key.transform.localPosition.x,
                key.transform.localPosition.y,
                key.transform.localPosition.z - distanceToCenter + RADIUS);
        }

        /// <summary>
        /// tan (x * 1,57) - tan is in range of <0,3.14>. With
        /// this approach we can scale it to range <0(0),1(close to infinity)>.
        /// Why + radious = 3?? because virtual radius of our circle is 3 
        /// google (tan(x*1.57) + 3) for visualization
        /// Higher values make center position further from keys (straight line)
        /// </summary>
        private void CurvatureToDistance()
        {
            distanceToCenter = Mathf.Tan((Curvature) * 1.57f) + RADIUS;
        }

        #endregion

        #region Properties

        public float SpacingBetweenKeys
        {
            get { return SPACING_COLUMM / distanceToCenter; }
        }

        public float Curvature
        {
            get { return 1f - curvature; }
        }

        public float DistanceToCenter
        {
            get { return distanceToCenter; }
        }

        #endregion

        public void changeCols(Slider slider)
        {
            SPACING_COLUMM = slider.value;
            UpdateKeys();
        }

        public void changeRows(Slider slider)
        {
            SPACING_ROW = slider.value;
            UpdateKeys();
        }

        public void changeCurve(Slider slider)
        {
            curvature = Mathf.Round(slider.value * 100f) / 100f;
            CurvatureToDistance();
            UpdateKeys();
        }

        public void changeSize(Slider slider)
        {
            KEY_SIZE = slider.value;
            UpdateKeys();
        }
    }
//}


