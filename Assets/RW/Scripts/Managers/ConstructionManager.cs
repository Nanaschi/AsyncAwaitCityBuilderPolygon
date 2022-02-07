/*
 * Copyright (c) 2021 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RayWenderlich.WenderlichTopia
{
    public class ConstructionManager : MonoBehaviour
    {
        public GameObject constructionTilePrefab;
        public UiManager uiManager;
        public Transform levelGeometryContainer;

        public async void BuildStructure(GameObject placementStructure, Vector3 buildPosition)
        {
            if (placementStructure.TryGetComponent(out RoadBuildPropertiesContainer roadBuildPropertiesContainer))
            {
                Destroy(placementStructure);
                var roadProperties = roadBuildPropertiesContainer.roadBuildProperties;
                var buildRoadTask = BuildRoadAsync(roadProperties, buildPosition);
                await buildRoadTask;
                uiManager.NewStructureComplete(roadProperties.roadCost, buildPosition);
            }
            
            else if (placementStructure.TryGetComponent(out HouseBuildPropertiesContainer houseBuildPropertiesContainer))
            {
                Destroy(placementStructure); 
                var houseProperties = houseBuildPropertiesContainer.houseBuildProperties;
                var buildHouseTask = BuildHouseAsync(houseProperties, buildPosition);
                await buildHouseTask; 
                var houseCost = buildHouseTask.Result;
                uiManager.NewStructureComplete(houseCost, buildPosition);
            }
        }
        
        private async Task BuildRoadAsync(RoadBuildProperties roadProperties, Vector3 buildPosition)
        {
            var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
            await Task.Delay(2500);
            Destroy(constructionTile);
            Instantiate(roadProperties.completedRoadPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);

        }
        private async Task<int> BuildHouseAsync(HouseBuildProperties houseBuildProperties, Vector3 buildPosition)
        {
            var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
            await Task.Delay(1000);
            return 100;
        }
    }
}