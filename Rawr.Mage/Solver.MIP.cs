﻿//#define DEBUG_BRANCHING
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr.Mage
{
    public partial class Solver
    {
#if DEBUG_BRANCHING
        private List<SolverLP> childList;
#endif

        private void RestrictSolution()
        {
#if DEBUG_BRANCHING
            childList = new List<SolverLP>();
#endif

            int maxHeap = calculationOptions.MaxHeapLimit;
            lp.SolvePrimalDual(); // solve primal and recalculate to get a stable starting point
            heap = new Heap<SolverLP>(HeapType.MaximumHeap);
            HeapPush(lp);

            double max = lp.Value;

            bool valid = true;
            do
            {
                if (heap.Head.Value > max + 0.001) // lowered instability threshold, in case it is still an issue just recompute the solution which "should" give a stable result hopefully
                {
                    // recovery measures first
                    double current = heap.Head.Value;
                    lp = heap.Pop();
                    lp.ForceRecalculation();
                    // some testing indicates that the recalculated solution gives the correct result, so the previous solution is most likely to be the problematic one, since we just discarded it not a big deal
                    //if (lp.Value <= max + 1.0)
                    //{
                    // give more fudge room in case the previous max was the one that was unstable
                    max = lp.Value;
                    HeapPush(lp);
                    continue;
                    //}
                    //System.Windows.Forms.MessageBox.Show("Instability detected, aborting SMP algorithm (max = " + max + ", value = " + lp.Value + ")");
                    // find something reasonably stable
                    //while (heap.Count > 0 && (lp = heap.Pop()).Value > max + 0.000001) { }
                    //break;
                }
                lp = heap.Pop();
                max = lp.Value;
                // this is the best non-evaluated option (highest partially-constrained LP, the optimum has to be lower)
                // if this one is valid than all others are sub-optimal
                // validate all segments for each cooldown
                solution = lp.Solve();
                /*System.Diagnostics.Trace.WriteLine("Solution basis (value = " + lp.Value + "):");
                for (int index = 0; index < lpCols; index++)
                {
                    if (solution[index] > 0.000001) System.Diagnostics.Trace.WriteLine(index);
                }*/
                if (heap.Count > maxHeap)
                {
                    System.Windows.Forms.MessageBox.Show("SMP algorithm exceeded maximum allowed computation limit. Displaying the last working solution. Increase the limit in options if you would like to compute the correct solution.");
                    break;
                }
                valid = true;

#if DEBUG_BRANCHING
                childList.Clear();
#endif

                if (integralMana)
                {
                    if (valid && calculationOptions.ManaPotionEnabled)
                    {
                        valid = ValidateIntegralConsumableOverall(VariableType.ManaPotion, 1.0);
                    }
                    if (valid && calculationOptions.ManaGemEnabled)
                    {
                        valid = ValidateIntegralConsumableOverall(VariableType.ManaGem, 1.0);
                    }
                    if (valid && calculationOptions.EvocationEnabled)
                    {
                        valid = ValidateIntegralConsumableOverall(VariableType.Evocation, 2.0 / calculationResult.BaseState.CastingSpeed);
                    }
                }

                if (segmentCooldowns)
                {
                    // drums
                    if (valid && calculationOptions.DrumsOfBattle)
                    {
                        valid = ValidateIntegralConsumableOverall(VariableType.DrumsOfBattle, calculationResult.BaseState.GlobalCooldown);
                    }
                    // drums
                    if (valid && calculationOptions.DrumsOfBattle)
                    {
                        valid = ValidateCooldown(Cooldown.DrumsOfBattle, 30.0, 120.0, true);
                    }
                    // make sure all cooldowns are tightly packed and not fragmented
                    // mf is trivially satisfied
                    // heroism
                    if (valid && heroismAvailable)
                    {
                        valid = ValidateCooldown(Cooldown.Heroism, 40, -1);
                    }
                    // ap
                    if (valid && arcanePowerAvailable)
                    {
                        valid = ValidateCooldown(Cooldown.ArcanePower, 15, 180);
                    }
                    // iv
                    if (valid && icyVeinsAvailable)
                    {
                        valid = ValidateCooldown(Cooldown.IcyVeins, 20 + (coldsnapAvailable ? 20 : 0), 180 + (coldsnapAvailable ? 20 : 0));
                    }
                    // combustion
                    if (valid && combustionAvailable)
                    {
                        valid = ValidateCooldown(Cooldown.Combustion, 15, 180 + 15); // the durations are only used to compute segment distances, for 30 sec segments this should work pretty well
                    }
                }

                if (integralMana)
                {
                    if (valid && calculationOptions.ManaPotionEnabled)
                    {
                        valid = ValidateIntegralConsumable(VariableType.ManaPotion);
                    }
                    if (valid && calculationOptions.ManaGemEnabled)
                    {
                        valid = ValidateIntegralConsumable(VariableType.ManaGem);
                    }
                }

                if (segmentCooldowns)
                {
                    // flamecap
                    if (valid && calculationOptions.FlameCap)
                    {
                        valid = ValidateCooldown(Cooldown.FlameCap, 60, 180, integralMana);
                    }
                    // destruction
                    if (valid && calculationOptions.DestructionPotion)
                    {
                        valid = ValidateCooldown(Cooldown.DestructionPotion, 15, 120);
                    }
                    // t1
                    if (valid && trinket1Available)
                    {
                        valid = ValidateCooldown(Cooldown.Trinket1, trinket1Duration, trinket1Cooldown);
                    }
                    // t2
                    if (valid && trinket2Available)
                    {
                        valid = ValidateCooldown(Cooldown.Trinket2, trinket2Duration, trinket2Cooldown);
                    }
                    /*if (valid && t1ismg && calculationOptions.FlameCap)
                    {
                        valid = ValidateSCB(Cooldown.Trinket1);
                    }
                    if (valid && t2ismg && calculationOptions.FlameCap)
                    {
                        valid = ValidateSCB(Cooldown.Trinket2);
                    }*/
                    if (valid && calculationOptions.DrumsOfBattle)
                    {
                        valid = ValidateActivation(Cooldown.DrumsOfBattle, 30.0, 120.0, VariableType.DrumsOfBattle);
                    }
                    if (valid)
                    {
                        valid = ValidateCycling();
                    }
                    if (valid && icyVeinsAvailable && coldsnapAvailable)
                    {
                        valid = ValidateColdsnap();
                    }
                    if (valid)
                    {
                        valid = ValidateSupergroupFragmentation();
                    }
                }

#if DEBUG_BRANCHING
                if (!valid)
                {
                    bool allLower = true;
                    foreach (SolverLP childLP in childList)
                    {
                        if (childLP.Value >= max - 1.0)
                        {
                            allLower = false;
                            break;
                        }
                    }
                    if (allLower && childList.Count > 0 && childList[0].Log != null)
                    {
                        System.Diagnostics.Trace.WriteLine("\n\nProves branch has nontrivially lower value:");
                        System.Diagnostics.Trace.Write(childList[0].Log.ToString());
                    }
                    childList.Clear();
                }
#endif
            } while (heap.Count > 0 && !valid);
            //System.Diagnostics.Trace.WriteLine("Heap at solution " + heap.Count);
        }

        private void HeapPush(SolverLP childLP)
        {
            heap.Push(childLP);
#if DEBUG_BRANCHING
            childList.Add(childLP);
#endif
        }

        private bool ValidateSupergroupFragmentation()
        {
            List<int>[] hexList = new List<int>[segments];
            double[] count = new double[segments];
            for (int seg = 0; seg < segments; seg++)
            {
                hexList[seg] = new List<int>();
                for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    int iseg = calculationResult.SolutionVariable[index].Segment;
                    if (state != null && solution[index] > 0 && iseg == seg)
                    {
                        int h = state.GetHex();
                        if (h != 0)
                        {
                            if (!hexList[seg].Contains(h)) hexList[seg].Add(h);
                            count[seg] += solution[index];
                        }
                    }
                }
            }
            for (int seg = 1; seg < segments - 1; seg++)
            {
                int N = hexList[seg].Count;
                if (N > 0 && count[seg] < segmentDuration - 0.000001)
                {
                    // if any hex links to left and right segment we have a problem
                    List<int> minHexChain = null;
                    for (int i = 0; i < N; i++)
                    {
                        // compute distance
                        int[] hexDistance = new int[N];
                        for (int j = 0; j < N; j++)
                        {
                            hexDistance[j] = -1;
                        }
                        hexDistance[i] = 0;
                        int current = hexList[seg][i];
                        int distance = 0;
                        int next;
                        bool unlinked = true;
                        while (unlinked)
                        {
                            unlinked = false;
                            distance++;
                            next = current;
                            for (int j = 0; j < N; j++)
                            {
                                if (hexDistance[j] == -1 && (hexList[seg][j] & current) != 0)
                                {
                                    hexDistance[j] = distance;
                                    next |= hexList[seg][j];
                                    unlinked = true;
                                }
                            }
                            current = next;
                        }
                        // find closest link on left and right
                        int leftMin = int.MaxValue;
                        int leftMinHex = 0;
                        for (int j = 0; j < hexList[seg - 1].Count; j++)
                        {
                            int h = hexList[seg - 1][j];
                            if ((h & current) != 0)
                            {
                                for (int k = 0; k < N; k++)
                                {
                                    if ((h & hexList[seg][k]) != 0)
                                    {
                                        if (hexDistance[k] != -1 && hexDistance[k] < leftMin)
                                        {
                                            leftMin = hexDistance[k];
                                            leftMinHex = h;
                                        }
                                    }
                                }
                            }
                        }
                        int rightMin = int.MaxValue;
                        int rightMinHex = 0;
                        for (int j = 0; j < hexList[seg + 1].Count; j++)
                        {
                            int h = hexList[seg + 1][j];
                            if ((h & current) != 0)
                            {
                                for (int k = 0; k < N; k++)
                                {
                                    if ((h & hexList[seg][k]) != 0)
                                    {
                                        if (hexDistance[k] != -1 && hexDistance[k] < rightMin)
                                        {
                                            rightMin = hexDistance[k];
                                            rightMinHex = h;
                                        }
                                    }
                                }
                            }
                        }
                        if (leftMinHex != 0 && rightMinHex != 0)
                        {
                            // we found an offensive hex chain
                            if (minHexChain == null || (leftMin + rightMin + 2) < minHexChain.Count)
                            {
                                // reconstruct chain
                                minHexChain = new List<int>();
                                int currentHex = leftMinHex;
                                int currentDist = leftMin;
                                if (!minHexChain.Contains(currentHex)) minHexChain.Add(currentHex);
                                while (currentDist > 0)
                                {
                                    for (int j = 0; j < N; j++)
                                    {
                                        if (hexDistance[j] == currentDist && (hexList[seg][j] & currentHex) != 0)
                                        {
                                            currentHex = hexList[seg][j];
                                            currentDist--;
                                            if (!minHexChain.Contains(currentHex)) minHexChain.Add(currentHex);
                                            break;
                                        }
                                    }
                                }
                                currentHex = rightMinHex;
                                currentDist = rightMin;
                                if (!minHexChain.Contains(currentHex)) minHexChain.Add(currentHex);
                                while (currentDist > 0)
                                {
                                    for (int j = 0; j < N; j++)
                                    {
                                        if (hexDistance[j] == currentDist && (hexList[seg][j] & currentHex) != 0)
                                        {
                                            currentHex = hexList[seg][j];
                                            currentDist--;
                                            if (!minHexChain.Contains(currentHex)) minHexChain.Add(currentHex);
                                            break;
                                        }
                                    }
                                }
                                currentHex = hexList[seg][i];
                                if (!minHexChain.Contains(currentHex)) minHexChain.Add(currentHex);
                            }
                        }
                    }
                    if (minHexChain != null)
                    {
                        // we have a problem and the shortest hex chain that identifies it
                        // solve by branching on eliminating one of the elements in the chain
                        // or forcing the segment to max
                        for (int i = 0; i < minHexChain.Count; i++)
                        {
                            SolverLP hexRemovedLP = lp.Clone();
                            if (hexRemovedLP.Log != null) hexRemovedLP.Log.AppendLine("Breaking supergroup fragmentation at " + seg + ", removing " + minHexChain[i]);
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                int iseg = calculationResult.SolutionVariable[index].Segment;
                                if (state != null && iseg >= seg - 1 && iseg <= seg + 1 && state.GetHex() == minHexChain[i]) hexRemovedLP.EraseColumn(index);
                            }
                            HeapPush(hexRemovedLP);
                        }
                        if (lp.Log != null) lp.Log.AppendLine("Breaking supergroup fragmentation at " + seg + ", force to max");
                        for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            int iseg = calculationResult.SolutionVariable[index].Segment;
                            if (state != null && iseg == seg && state.GetHex() != 0) lp.UpdateMaximizeSegmentColumn(index);
                        }
                        lp.UpdateMaximizeSegmentDuration(segmentDuration);
                        HeapPush(lp);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ValidateCycling()
        {
            // eliminate packing cycles
            // example:
            // H+IV:10
            // IV+Icon:10
            // H+Icon:10
            bool valid = true;
            for (int seg = 0; seg < segments - 1; seg++)
            {
                // collect all cooldowns on the boundary seg...(seg+1)
                // assume one instance of cooldown max (coldsnap theoretically doesn't satisfy this, but I think it should still work)
                // calculate hex values for boolean arithmetic
                // verify if there are cycles

                // ###   ###
                // ######
                //    ######

                // ##
                //  ##
                //   ##
                //    ##
                //     ##
                // #    #

                // cycle = no element can be placed at the start, all have two tails that intersect to 0
                // inside the boundary we can have more than one cycle and several nice packings
                // find elements that can be placed at the start, those are the ones with nice packing
                // for each one you find remove the corresponding group
                // if we remove everything then there are no cycles

                List<int> hexList = new List<int>();
                List<int> hexList1 = new List<int>();
                List<int> hexList2 = new List<int>();
                for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    int iseg = calculationResult.SolutionVariable[index].Segment;
                    if (state != null && solution[index] > 0)
                    {
                        if (iseg == seg || iseg == seg + 1)
                        {
                            int h = state.GetHex();
                            if (h != 0)
                            {
                                if (!hexList.Contains(h)) hexList.Add(h);
                                if (iseg == seg && !hexList1.Contains(h)) hexList1.Add(h);
                                if (iseg == seg + 1 && !hexList2.Contains(h)) hexList2.Add(h);
                            }
                        }
                    }
                }

                // placed  ## ### ## #
                //         .. ...  
                //          .   . .. .
                // active   #   #    #
                //
                // future   #   ##      <= ok
                //          #   #  #    <= not ok

                // take newHex = (future - future & active)
                // if newHex & placed > 0 then we have cycle

                bool ok = true;
                int placed = 0;
                while (ok && hexList.Count > 0)
                {
                    ok = false;
                    // check if any can be at the start
                    for (int i = 0; i < hexList.Count; i++)
                    {
                        int tail = hexList[i];
                        for (int j = 0; j < hexList.Count; j++)
                        {
                            int intersect = hexList[i] & hexList[j];
                            if (i != j && intersect > 0)
                            {
                                tail &= hexList[j];
                                if (tail == 0) break;
                            }
                            int newHex = hexList[j] - intersect;
                            if ((newHex & placed) > 0)
                            {
                                tail = 0;
                                break;
                            }
                        }
                        if (tail != 0)
                        {
                            // i is good
                            ok = true;
                            placed |= hexList[i];
                            hexList.RemoveAt(i);
                            break;
                        }
                    }
                }
                if (hexList.Count > 0)
                {
                    // we have a cycle
                    // to break the cycle we have to remove one of the elements
                    // if all are present then obviously we have a cycle, so the true solution must have one of them missing
                    for (int i = 0; i < hexList.Count; i++)
                    {
                        SolverLP hexRemovedLP = lp.Clone();
                        if (hexRemovedLP.Log != null) hexRemovedLP.Log.AppendLine("Breaking cycle at boundary " + seg + ", removing " + hexList[i]);
                        for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            int iseg = calculationResult.SolutionVariable[index].Segment;
                            if (state != null && state.GetHex() == hexList[i] && (iseg == seg || iseg == seg + 1)) hexRemovedLP.EraseColumn(index);
                        }
                        HeapPush(hexRemovedLP);
                    }
                    valid = false;
                    break;
                }
                // detect and eliminate double crossings
                // #X |X #
                // # Y| Y#
                // problem happens if we have X without Y and Y without X in one segment and X and Y
                // somewhere in the other segment
                // so either X-Y can't exist or Y-X or X and Y in the other segment
                // eliminate by 4-way branch
                int hex1 = 0;
                for (int i = 0; i < hexList1.Count; i++)
                {
                    hex1 |= hexList1[i];
                }
                int hex2 = 0;
                for (int i = 0; i < hexList2.Count; i++)
                {
                    hex2 |= hexList2[i];
                }
                int hex = hex1 & hex2; // crossings
                int cool1 = 0, cool2 = 0, seg1 = 0, seg2 = 0;
                for (int i = 0; i < hexList1.Count; i++)
                {
                    for (int j = i + 1; j < hexList1.Count; j++)
                    {
                        placed = hex & hexList1[i];
                        int h = hex & hexList1[j];
                        int hp = h & placed;
                        if (placed != hp && h != hp)
                        {
                            // XXX XX   placed
                            //  XXXX    h
                            //  .. .    hp
                            // #  - #   placed - hp
                            // -  # -   h - hp
                            valid = false;
                            for (cool1 = 0; cool1 < cooldownCount; cool1++)
                            {
                                if (((1 << cool1) & placed & ~hp) != 0) break;
                            }
                            for (cool2 = 0; cool2 < cooldownCount; cool2++)
                            {
                                if (((1 << cool2) & h & ~hp) != 0) break;
                            }
                            seg1 = seg;
                            seg2 = seg + 1;
                            goto ELIMINATECROSSING;
                        }
                    }
                }
                for (int i = 0; i < hexList2.Count; i++)
                {
                    for (int j = i + 1; j < hexList2.Count; j++)
                    {
                        placed = hex & hexList2[i];
                        int h = hex & hexList2[j];
                        int hp = h & placed;
                        if (placed != hp && h != hp)
                        {
                            // XXX XX   placed
                            //  XXXX    h
                            //  .. .    hp
                            // #  - #   placed - hp
                            // -  # -   h - hp
                            valid = false;
                            for (cool1 = 0; cool1 < cooldownCount; cool1++)
                            {
                                if (((1 << cool1) & placed & ~hp) != 0) break;
                            }
                            for (cool2 = 0; cool2 < cooldownCount; cool2++)
                            {
                                if (((1 << cool2) & h & ~hp) != 0) break;
                            }
                            seg1 = seg + 1;
                            seg2 = seg;
                            goto ELIMINATECROSSING;
                        }
                    }
                }
            ELIMINATECROSSING:
                if (!valid)
                {
                    int c1 = 1 << cool1;
                    int c2 = 1 << cool2;
                    // eliminate cool1 - cool2 in seg1
                    SolverLP elimLP = lp.Clone();
                    for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        int iseg = calculationResult.SolutionVariable[index].Segment;
                        if (state != null && iseg == seg1)
                        {
                            int h = state.GetHex();
                            if ((h & c1) != 0 && (h & c2) == 0) elimLP.EraseColumn(index);
                        }
                    }
                    if (elimLP.Log != null) elimLP.Log.AppendLine("Doublecrossing at " + seg1 + ", " + cool1 + " - " + cool2);
                    HeapPush(elimLP);
                    // eliminate cool2 - cool1 in seg1
                    elimLP = lp.Clone();
                    for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        int iseg = calculationResult.SolutionVariable[index].Segment;
                        if (state != null && iseg == seg1)
                        {
                            int h = state.GetHex();
                            if ((h & c2) != 0 && (h & c1) == 0) elimLP.EraseColumn(index);
                        }
                    }
                    if (elimLP.Log != null) elimLP.Log.AppendLine("Doublecrossing at " + seg1 + ", " + cool2 + " - " + cool1);
                    HeapPush(elimLP);
                    // eliminate cool1 in seg2
                    elimLP = lp.Clone();
                    for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        int iseg = calculationResult.SolutionVariable[index].Segment;
                        if (state != null && iseg == seg2)
                        {
                            int h = state.GetHex();
                            if ((h & c1) != 0) elimLP.EraseColumn(index);
                        }
                    }
                    if (elimLP.Log != null) elimLP.Log.AppendLine("Doublecrossing at " + seg1 + "+1, delete " + cool1);
                    HeapPush(elimLP);
                    // eliminate cool2 in seg2
                    elimLP = lp;
                    for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        int iseg = calculationResult.SolutionVariable[index].Segment;
                        if (state != null && iseg == seg2)
                        {
                            int h = state.GetHex();
                            if ((h & c2) != 0) elimLP.EraseColumn(index);
                        }
                    }
                    if (elimLP.Log != null) elimLP.Log.AppendLine("Doublecrossing at " + seg1 + "+1, delete " + cool2);
                    HeapPush(elimLP);
                    break;
                }
            }
            return valid;
        }

        private bool ValidateIntegralConsumable(VariableType integralConsumable)
        {
            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
            {
                if (calculationResult.SolutionVariable[index].Type == integralConsumable)
                {
                    double value = solution[index];
                    int count = (int)Math.Round(value);
                    bool valid = (Math.Abs(value - count) < 0.000001);

                    if (!valid)
                    {
                        SolverLP maxCount = lp.Clone();
                        // count <= floor(value)
                        maxCount.SetColumnUpperBound(index, Math.Floor(value));
                        maxCount.ForceRecalculation(true);
                        if (maxCount.Log != null) maxCount.Log.AppendLine("Integral consumable " + integralConsumable + " at " + calculationResult.SolutionVariable[index].Segment + ", max " + Math.Floor(value));
                        HeapPush(maxCount);
                        // count >= ceiling(value)
                        lp.SetColumnLowerBound(index, Math.Ceiling(value));
                        lp.ForceRecalculation(true);
                        if (lp.Log != null) lp.Log.AppendLine("Integral consumable " + integralConsumable + " at " + calculationResult.SolutionVariable[index].Segment + ", min " + Math.Ceiling(value));
                        HeapPush(lp);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ValidateIntegralConsumableOverall(VariableType integralConsumable, double unit)
        {
            double value = 0.0;
            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
            {
                if (calculationResult.SolutionVariable[index].Type == integralConsumable)
                {
                    value += solution[index];
                }
            }
            double count = Math.Round(value / unit) * unit;
            bool valid = (Math.Abs(value - count) < 0.000001);
            int row = -1;
            switch (integralConsumable)
            {
                case VariableType.ManaGem:
                    row = rowManaGemOnly;
                    break;
                case VariableType.ManaPotion:
                    row = rowManaPotion;
                    break;
                case VariableType.Evocation:
                    row = rowEvocation;
                    break;
                case VariableType.DrumsOfBattle:
                    row = rowDrumsOfBattle;
                    break;
            }
            if (!valid)
            {
                SolverLP maxCount = lp.Clone();
                // count <= floor(value)
                maxCount.SetRHS(row, Math.Floor(value / unit) * unit);
                maxCount.ForceRecalculation(true);
                if (maxCount.Log != null) maxCount.Log.AppendLine("Integral consumable " + integralConsumable + " overall, max " + Math.Floor(value / unit));
                HeapPush(maxCount);
                // count >= ceiling(value)
                lp.SetLHS(row, Math.Ceiling(value / unit) * unit);
                lp.ForceRecalculation(true);
                if (lp.Log != null) lp.Log.AppendLine("Integral consumable " + integralConsumable + " overall, min " + Math.Ceiling(value / unit));
                HeapPush(lp);
                return false;
            }
            return true;
        }

        private void EnsureColdsnapConstraints()
        {
            if (!lp.HasColdsnapConstraints)
            {
                lp.AddColdsnapConstraints(segments + (int)(180.0 / segmentDuration) - 1);
                for (int seg = 0; seg < segments + (int)(180.0 / segmentDuration) - 1; seg++)
                {
                    for (int outseg = Math.Max(0, (int)Math.Ceiling(seg + 1 - 180.0 / segmentDuration - 0.000001)); outseg <= Math.Min(seg, segments - 1); outseg++)
                    {
                        for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(Cooldown.IcyVeins))
                            {
                                lp.UpdateColdsnapColumn(seg, index);
                            }
                        }
                    }
                    lp.UpdateColdsnapDuration(seg, 40.0);
                }
            }
        }

        private bool ValidateColdsnap()
        {
            const double eps = 0.000001;
            double[] segCount = new double[segments];
            for (int outseg = 0; outseg < segments; outseg++)
            {
                double s = 0.0;
                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.GetCooldown(Cooldown.IcyVeins))
                    {
                        s += solution[index];
                    }
                }
                segCount[outseg] = s;
            }
            // everything is valid, except possibly coldsnap so we can assume the effects are nicely packed
            // check where coldsnaps are needed, similar to evaluation in sequence reconstruction
            bool ivActive = false;
            double lastIVstart = 0.0;
            double coldsnapTimer = 0.0;
            double ivCooldown = 0.0;
            bool valid = true;
            double coldsnapActivation = 0.0;
            for (int seg = 0; seg < segments; seg++)
            {
                if (ivActive)
                {
                    if (segCount[seg] > 0.0)
                    {
                        if (seg * segmentDuration + segCount[seg] > lastIVstart + 20.0 + eps)
                        {
                            if (coldsnapTimer <= (lastIVstart + 20.0 - seg * segmentDuration) + eps)
                            {
                                coldsnapActivation = Math.Max(seg * segmentDuration + coldsnapTimer, lastIVstart);
                                lastIVstart += 20.0;
                                ivCooldown += 20.0;
                                coldsnapTimer = coldsnapCooldown - (seg * segmentDuration - coldsnapActivation);
                            }
                            if (seg * segmentDuration + segCount[seg] > lastIVstart + 20.0 + eps)
                            {
                                // we need to coldsnap iv, but it is on cooldown
                                valid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ivActive = false;
                    }
                }
                else
                {
                    if (segCount[seg] > 0.0)
                    {
                        if (ivCooldown + segCount[seg] > segmentDuration + eps && coldsnapTimer + segCount[seg] > segmentDuration + eps)
                        {
                            // iv cooldown not ready and coldsnap won't be ready in time
                            valid = false;
                            break;
                        }
                        else
                        {
                            if (ivCooldown + segCount[seg] > segmentDuration + eps && coldsnapTimer + segCount[seg] <= segmentDuration + eps)
                            {
                                // iv not ready, but we can coldsnap
                                coldsnapActivation = Math.Max(seg * segmentDuration + coldsnapTimer, lastIVstart);
                                coldsnapTimer = coldsnapCooldown - (seg * segmentDuration - coldsnapActivation);
                                double ivActivation = Math.Max(coldsnapActivation, seg * segmentDuration);
                                ivCooldown = 180.0 + ivActivation - seg * segmentDuration;
                                ivActive = true;
                                lastIVstart = ivActivation;
                            }
                            else
                            {
                                // start as soon as possible
                                double ivActivation = Math.Max(seg * segmentDuration + ivCooldown, seg * segmentDuration);
                                if (seg + 1 < segments && segCount[seg + 1] > 0) ivActivation = (seg + 1) * segmentDuration - segCount[seg];
                                ivCooldown = 180.0 + ivActivation - seg * segmentDuration;
                                ivActive = true;
                                lastIVstart = ivActivation;
                            }
                            if (segCount[seg] > 20.0 + eps)
                            {
                                if (coldsnapTimer <= (lastIVstart + 20.0 - seg * segmentDuration) + eps)
                                {
                                    coldsnapActivation = Math.Max(seg * segmentDuration + coldsnapTimer, lastIVstart);
                                    lastIVstart += 20.0;
                                    ivCooldown += 20.0;
                                    coldsnapTimer = coldsnapCooldown - (seg * segmentDuration - coldsnapActivation);
                                }
                                if (seg * segmentDuration + segCount[seg] > lastIVstart + 20.0 + eps)
                                {
                                    // we need to coldsnap iv, but it is on cooldown
                                    valid = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ivActive = false;
                    }
                }
                coldsnapTimer -= segmentDuration;
                ivCooldown -= segmentDuration;
            }
            if (!valid)
            {
                EnsureColdsnapConstraints();
                int coldsnapSegment = (int)(coldsnapActivation / segmentDuration);
                // branch on whether we have coldsnap in this segment or not
                SolverLP coldsnapUsedIntra = lp.Clone();
                SolverLP coldsnapUsedExtra = lp.Clone();
                SolverLP zerolp = lp.Clone();
                // if we don't use coldsnap then this segment should be either zeroed or we have to restrict the segments until next iv (I think (tm))
                for (int seg = coldsnapSegment; seg < Math.Min(coldsnapSegment + (int)Math.Ceiling(180.0 / segmentDuration) + 1, segments + (int)(180.0 / segmentDuration) - 1); seg++) // it's possible we're overrestricting here, might have to add another branch where we go to one less but zero out seg+1
                {
                    lp.UpdateColdsnapDuration(seg, 20.0);
                }
                HeapPush(lp);
                for (int index = segmentColumn[coldsnapSegment]; index < segmentColumn[coldsnapSegment + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.GetCooldown(Cooldown.IcyVeins)) zerolp.EraseColumn(index);
                }
                HeapPush(zerolp);
                // coldsnap used extra
                // the segments for the duration of one IV can be loose, but all after that have to be restricted until coldsnap is ready
                coldsnapUsedExtra.UpdateColdsnapDuration(coldsnapSegment, 20.0);
                int firstRestrictedSeg = coldsnapSegment + 1 + (int)Math.Ceiling(180.0 / segmentDuration);
                int nextPossibleColdsnap = coldsnapSegment + (int)(coldsnapCooldown / segmentDuration);
                int segLimit = Math.Min(segments + (int)(180.0 / segmentDuration) - 1, nextPossibleColdsnap);
                for (int seg = firstRestrictedSeg; seg < segLimit; seg++)
                {
                    coldsnapUsedExtra.UpdateColdsnapDuration(seg, 20.0);
                }
                HeapPush(coldsnapUsedExtra);
                for (int seg = Math.Min(coldsnapSegment + 2, segments - 1); seg < coldsnapSegment + (int)(180.0 / segmentDuration); seg++)
                {
                    for (int index = segmentColumn[seg]; index < segmentColumn[seg + 1]; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && state.GetCooldown(Cooldown.IcyVeins)) coldsnapUsedIntra.EraseColumn(index);
                    }
                }
                for (int seg = firstRestrictedSeg; seg < segLimit; seg++)
                {
                    coldsnapUsedIntra.UpdateColdsnapDuration(seg, 20.0);
                }
                HeapPush(coldsnapUsedIntra);
                return false;
            }
            return valid;
        }

        private bool ValidateCooldown(Cooldown cooldown, double effectDuration, double cooldownDuration)
        {
            return ValidateCooldown(cooldown, effectDuration, cooldownDuration, false);
        }

        private bool ValidateActivation(Cooldown cooldown, double effectDuration, double cooldownDuration, VariableType activation)
        {
            const double eps = 0.00001;
            double[] segCount = new double[segments];
            double[] segActivation = new double[segments];
            for (int outseg = 0; outseg < segments; outseg++)
            {
                double s = 0.0;
                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.GetCooldown(cooldown))
                    {
                        s += solution[index];
                    }
                }
                segCount[outseg] = s;
            }
            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
            {
                CastingState state = calculationResult.SolutionVariable[index].State;
                if (state != null && state.GetCooldown(cooldown)) segCount[calculationResult.SolutionVariable[index].Segment] += solution[index];
            }
            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
            {
                if (calculationResult.SolutionVariable[index].Type == activation) segActivation[calculationResult.SolutionVariable[index].Segment] += solution[index];
            }
            int mindist = (int)Math.Ceiling(effectDuration / segmentDuration);
            int mindist2 = (int)Math.Floor(effectDuration / segmentDuration);
            int maxdist = (cooldownDuration < 0) ? 3 * segments : ((int)Math.Floor((cooldownDuration - effectDuration) / segmentDuration));
            int maxdist2 = (cooldownDuration < 0) ? 3 * segments : ((int)Math.Floor(cooldownDuration / segmentDuration));

            bool valid = true;

            for (int seg = 0; seg < segments; seg++)
            {
                if (segActivation[seg] > eps)
                {
                    for (int s = 0; s < segments; s++)
                    {
                        if (Math.Abs(seg - s) <= mindist && s < seg && segCount[s] > eps) valid = false; 
                    }
                    if (!valid)
                    {
                        // can't have effect before activation
                        // either activation is not here or effect is not before it
                        SolverLP cooldownUsed = lp.Clone();
                        // activation not used
                        if (lp.Log != null) lp.Log.AppendLine("Disable activation of " + activation.ToString() + " at " + seg);
                        for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                        {
                            if (calculationResult.SolutionVariable[index].Type == activation && calculationResult.SolutionVariable[index].Segment == seg) lp.EraseColumn(index);
                        }
                        HeapPush(lp);
                        if (cooldownUsed.Log != null) cooldownUsed.Log.AppendLine("No " + cooldown.ToString() + " before activation at " + seg);
                        for (int s = 0; s < segments; s++)
                        {
                            if (Math.Abs(seg - s) <= mindist && s < seg)
                            {
                                for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                                {
                                    CastingState state = calculationResult.SolutionVariable[index].State;
                                    if (state != null && state.GetCooldown(cooldown)) cooldownUsed.EraseColumn(index);
                                }
                            }
                        }
                        for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(cooldown))
                            {
                                int outseg = calculationResult.SolutionVariable[index].Segment;
                                if (Math.Abs(seg - outseg) <= mindist && outseg < seg) cooldownUsed.EraseColumn(index);
                            }
                        }
                        HeapPush(cooldownUsed);
                        return false;
                    }
                }
            }

            for (int i = 0; i < segmentColumn[0]; i++) // fix if variable ordering changes
            {
                if (calculationResult.SolutionVariable[i].Type == activation && solution[i] > eps)
                {
                    int seg = calculationResult.SolutionVariable[i].Segment;
                    int seghex = calculationResult.SolutionVariable[i].State.GetHex();
                    // check all cooldowns that link to this drums activation
                    // if any of them is also before this segment we have a cycle if it's not present at activation
                    int linkedHex = 0;
                    for (int s = 0; s < segments; s++)
                    {
                        if (Math.Abs(seg - s) <= mindist)
                        {
                            for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown) && solution[index] > eps)
                                {
                                    linkedHex |= state.GetHex();
                                }
                            }
                        }
                    }
                    for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && state.GetCooldown(cooldown) && solution[index] > eps)
                        {
                            linkedHex |= state.GetHex();
                        }
                    }

                    int brokenHex = 0;
                    for (int s = 0; s < segments; s++)
                    {
                        if (s == seg - 1)
                        {
                            for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && solution[index] > eps)
                                {
                                    int h = state.GetHex() & linkedHex & ~seghex;
                                    if (h != 0) brokenHex = h;
                                }
                            }
                        }
                    }
                    for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && solution[index] > eps)
                        {
                            int outseg = calculationResult.SolutionVariable[index].Segment;
                            if (outseg == seg - 1)
                            {
                                int h = state.GetHex() & linkedHex & ~seghex;
                                if (h != 0) brokenHex = h;
                            }
                        }
                    }
                    if (brokenHex != 0)
                    {
                        // either we don't have activation that is without broken hex or drums casting with hex or hex before activation
                        SolverLP drumsnohex = lp.Clone();
                        if (drumsnohex.Log != null) drumsnohex.Log.AppendLine(cooldown.ToString() + " without hex");
                        for (int s = 0; s < segments; s++)
                        {
                            if (Math.Abs(seg - s) <= mindist)
                            {
                                for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                                {
                                    CastingState state = calculationResult.SolutionVariable[index].State;
                                    if (state != null && state.GetCooldown(cooldown) && (state.GetHex() & brokenHex) != 0) drumsnohex.EraseColumn(index);
                                }
                            }
                        }
                        HeapPush(drumsnohex);

                        SolverLP nohex = lp.Clone();
                        if (nohex.Log != null) nohex.Log.AppendLine("No hex");
                        for (int s = 0; s < segments; s++)
                        {
                            if (s == seg - 1)
                            {
                                for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                                {
                                    CastingState state = calculationResult.SolutionVariable[index].State;
                                    if (state != null && (state.GetHex() & brokenHex) != 0) nohex.EraseColumn(index);
                                }
                            }
                        }
                        HeapPush(nohex);

                        if (lp.Log != null) lp.Log.AppendLine("Disable activation of " + activation.ToString() + " at " + seg + " without hex");
                        for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                        {
                            if (calculationResult.SolutionVariable[index].Type == activation && calculationResult.SolutionVariable[index].Segment == seg && (calculationResult.SolutionVariable[index].State.GetHex() & brokenHex) != brokenHex) lp.EraseColumn(index);
                        }
                        HeapPush(lp);
                    }
                }
            }

            return true;
        }

        private bool ValidateCooldown(Cooldown cooldown, double effectDuration, double cooldownDuration, bool needsFullEffect)
        {
            const double eps = 0.00001;
            double[] segCount = new double[segments];
            for (int outseg = 0; outseg < segments; outseg++)
            {
                double s = 0.0;
                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.GetCooldown(cooldown))
                    {
                        s += solution[index];
                    }
                }
                segCount[outseg] = s;
            }
            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
            {
                CastingState state = calculationResult.SolutionVariable[index].State;
                if (state != null && state.GetCooldown(cooldown)) segCount[calculationResult.SolutionVariable[index].Segment] += solution[index];
            }
            int mindist = (int)Math.Ceiling(effectDuration / segmentDuration);
            int mindist2 = (int)Math.Floor(effectDuration / segmentDuration);
            int maxdist = (cooldownDuration < 0) ? 3 * segments : ((int)Math.Floor((cooldownDuration - effectDuration) / segmentDuration));
            int maxdist2 = (cooldownDuration < 0) ? 3 * segments : ((int)Math.Floor(cooldownDuration / segmentDuration));

            bool valid = true;

            if (needsFullEffect)
            {
                for (int seg = 0; seg < segments; seg++)
                {
                    if (segCount[seg] > 0.0 && (seg + 1) * segmentDuration + effectDuration < calculationOptions.FightDuration)
                    {
                        double total = 0.0;
                        for (int s = 0; s < segments; s++)
                        {
                            if (Math.Abs(seg - s) <= mindist) total += segCount[s];
                        }
                        if (total < effectDuration - eps)
                        {
                            SolverLP cooldownUsed = lp.Clone();
                            // cooldown not used
                            if (lp.Log != null) lp.Log.AppendLine("Disable " + cooldown.ToString() + " at " + seg);
                            for (int index = segmentColumn[seg]; index < segmentColumn[seg + 1]; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown)) lp.EraseColumn(index);
                            }
                            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown) && calculationResult.SolutionVariable[index].Segment == seg) lp.EraseColumn(index);
                            }
                            HeapPush(lp);
                            if (cooldownUsed.Log != null) cooldownUsed.Log.AppendLine("Force full " + cooldown.ToString() + " at " + seg);
                            for (int s = 0; s < segments; s++)
                            {
                                if (Math.Abs(seg - s) <= mindist)
                                {
                                    for (int index = segmentColumn[s]; index < segmentColumn[s + 1]; index++)
                                    {
                                        CastingState state = calculationResult.SolutionVariable[index].State;
                                        if (state != null)
                                        {
                                            if (state.GetCooldown(cooldown)) cooldownUsed.UpdateMaximizeSegmentColumn(index);
                                        }
                                    }
                                }
                            }
                            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown))
                                {
                                    int outseg = calculationResult.SolutionVariable[index].Segment;
                                    if (Math.Abs(seg - outseg) <= mindist) cooldownUsed.UpdateMaximizeSegmentColumn(index);
                                }
                            }
                            cooldownUsed.UpdateMaximizeSegmentDuration(effectDuration);
                            HeapPush(cooldownUsed);
                            return false;
                        }
                    }
                }
            }

            for (int seg = 0; seg < segments; seg++)
            {
                double inseg = segCount[seg];
                double mxdist = (seg == 0 ? maxdist2 : maxdist); // effect started in first segment couldn't be started earlier
                if (inseg > 0)
                {
                    // verify that outside duration segments are 0
                    for (int outseg = 0; outseg < segments; outseg++)
                    {
                        if (Math.Abs(outseg - seg) > mindist && Math.Abs(outseg - seg) < mxdist)
                        {
                            if (segCount[outseg] > 0)
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    if (!valid)
                    {
                        //if (lp.disabledHex == null) lp.disabledHex = new int[CooldownMax];
                        // branch on whether cooldown is used in this segment
                        SolverLP cooldownUsed = lp.Clone();
                        // cooldown not used
                        //lp.IVHash += 1 << seg;
                        if (lp.Log != null) lp.Log.AppendLine("Disable " + cooldown.ToString() + " at " + seg);
                        for (int index = segmentColumn[seg]; index < segmentColumn[seg + 1]; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(cooldown)) lp.EraseColumn(index);
                        }
                        for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(cooldown) && calculationResult.SolutionVariable[index].Segment == seg) lp.EraseColumn(index);
                        }
                        HeapPush(lp);
                        // cooldown used
                        if (cooldownUsed.Log != null) cooldownUsed.Log.AppendLine("Use " + cooldown.ToString() + " at " + seg + ", disable around");
                        for (int outseg = 0; outseg < segments; outseg++)
                        {
                            if (Math.Abs(outseg - seg) > mindist && Math.Abs(outseg - seg) < mxdist)
                            {
                                //cooldownUsed.IVHash += 1 << outseg;
                                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                                {
                                    CastingState state = calculationResult.SolutionVariable[index].State;
                                    if (state != null && state.GetCooldown(cooldown)) cooldownUsed.EraseColumn(index);
                                }
                            }
                        }
                        for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(cooldown))
                            {
                                int outseg = calculationResult.SolutionVariable[index].Segment;
                                if (Math.Abs(outseg - seg) > mindist && Math.Abs(outseg - seg) < mxdist) cooldownUsed.EraseColumn(index);
                            }
                        }
                        HeapPush(cooldownUsed);
                        return false;
                    }
                }
            }

            // fragmentation detection, replaced with hole detection
            /*for (int seg = 0; seg < segments; seg++)
            {
                double inseg = segCount[seg];
                double leftseg = 0.0;
                double rightseg = 0.0;
                for (int outseg = 0; outseg < segments; outseg++)
                {
                    if (Math.Abs(outseg - seg) <= mindist && outseg != seg)
                    {
                        if (outseg < seg)
                        {
                            leftseg += segCount[outseg];
                        }
                        else if (outseg > seg)
                        {
                            rightseg += segCount[outseg];
                        }
                    }
                }
                if (valid && inseg > 0.000001 && inseg < segmentDuration - 0.000001 && leftseg > 0 && rightseg > 0) // coldsnapped icy veins doesn't have to be contiguous, but getting better results assuming it is
                {
                    // fragmentation
                    // either left must be disabled, right disabled, or seg to max
                    SolverLP leftDisabled = lp.Clone();
                    if (leftDisabled.Log != null) leftDisabled.Log.AppendLine("Disable " + cooldown.ToString() + " left of " + seg);
                    for (int outseg = 0; outseg < segments; outseg++)
                    {
                        if ((outseg < seg || Math.Abs(outseg - seg) > mindist) && Math.Abs(outseg - seg) < maxdist)
                        {
                            for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown)) leftDisabled.EraseColumn(index);
                            }
                        }
                    }
                    for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && state.GetCooldown(cooldown))
                        {
                            int outseg = calculationResult.SolutionVariable[index].Segment;
                            if ((outseg < seg || Math.Abs(outseg - seg) > mindist) && Math.Abs(outseg - seg) < maxdist) leftDisabled.EraseColumn(index);
                        }
                    }
                    HeapPush(leftDisabled);
                    SolverLP rightDisabled = lp.Clone();
                    if (rightDisabled.Log != null) rightDisabled.Log.AppendLine("Disable " + cooldown.ToString() + " right of " + seg);
                    for (int outseg = 0; outseg < segments; outseg++)
                    {
                        if ((outseg > seg || Math.Abs(outseg - seg) > mindist) && Math.Abs(outseg - seg) < maxdist)
                        {
                            for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown)) rightDisabled.EraseColumn(index);
                            }
                        }
                    }
                    for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && state.GetCooldown(cooldown))
                        {
                            int outseg = calculationResult.SolutionVariable[index].Segment;
                            if ((outseg > seg || Math.Abs(outseg - seg) > mindist) && Math.Abs(outseg - seg) < maxdist) rightDisabled.EraseColumn(index);
                        }
                    }
                    HeapPush(rightDisabled);
                    if (lp.Log != null) lp.Log.AppendLine("Force " + cooldown.ToString() + " to max at " + seg);
                    for (int index = segmentColumn[seg]; index < segmentColumn[seg + 1]; index++)
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null)
                        {
                            if (state.GetCooldown(cooldown)) lp.UpdateMaximizeSegmentColumn(index);
                        }
                    }
                    for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                    {
                        CastingState state = calculationResult.SolutionVariable[index].State;
                        if (state != null && state.GetCooldown(cooldown))
                        {
                            int outseg = calculationResult.SolutionVariable[index].Segment;
                            if (outseg == seg) lp.UpdateMaximizeSegmentColumn(index);
                        }
                    }
                    lp.UpdateMaximizeSegmentDuration(segmentDuration);
                    HeapPush(lp);
                    return false;
                }
            }*/

            // detect holes
            for (int seg = 0; seg < segments; seg++)
            {
                double inseg = segCount[seg];
                if (inseg > 0)
                {
                    int lastseg = -1;
                    for (int outseg = seg + 1; outseg < Math.Min(segments, seg + mindist + 1); outseg++)
                    {
                        if (segCount[outseg] > 0) lastseg = outseg;
                    }
                    if (lastseg != -1)
                    {
                        for (int outseg = seg + 1; outseg < lastseg; outseg++)
                        {
                            if (segCount[outseg] < segmentDuration - eps) valid = false;
                        }
                        if (!valid) // coldsnapped icy veins doesn't have to be contiguous, but getting better results assuming it is
                        {
                            // either seg must be disabled, lastseg disabled, or middle to max
                            SolverLP leftDisabled = lp.Clone();
                            if (leftDisabled.Log != null) leftDisabled.Log.AppendLine("Disable left " + cooldown.ToString() + " at " + seg);
                            for (int outseg = 0; outseg < segments; outseg++)
                            {
                                if (outseg == seg)
                                {
                                    for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                                    {
                                        CastingState state = calculationResult.SolutionVariable[index].State;
                                        if (state != null && state.GetCooldown(cooldown)) leftDisabled.EraseColumn(index);
                                    }
                                }
                            }
                            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown))
                                {
                                    int outseg = calculationResult.SolutionVariable[index].Segment;
                                    if (outseg == seg) leftDisabled.EraseColumn(index);
                                }
                            }
                            HeapPush(leftDisabled);
                            SolverLP rightDisabled = lp.Clone();
                            if (rightDisabled.Log != null) rightDisabled.Log.AppendLine("Disable right " + cooldown.ToString() + " at " + lastseg);
                            for (int outseg = 0; outseg < segments; outseg++)
                            {
                                if (outseg == lastseg)
                                {
                                    for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                                    {
                                        CastingState state = calculationResult.SolutionVariable[index].State;
                                        if (state != null && state.GetCooldown(cooldown)) rightDisabled.EraseColumn(index);
                                    }
                                }
                            }
                            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown))
                                {
                                    int outseg = calculationResult.SolutionVariable[index].Segment;
                                    if (outseg == lastseg) rightDisabled.EraseColumn(index);
                                }
                            }
                            HeapPush(rightDisabled);
                            if (lp.Log != null) lp.Log.AppendLine("Force " + cooldown.ToString() + " to max from " + seg + " to " + lastseg);
                            for (int outseg = 0; outseg < segments; outseg++)
                            {
                                if (outseg > seg && outseg < lastseg)
                                {
                                    for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                                    {
                                        CastingState state = calculationResult.SolutionVariable[index].State;
                                        if (state != null && state.GetCooldown(cooldown)) lp.UpdateMaximizeSegmentColumn(index);                                        
                                    }
                                }
                            }
                            for (int index = 0; index < segmentColumn[0]; index++) // fix if variable ordering changes
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (state != null && state.GetCooldown(cooldown))
                                {
                                    int outseg = calculationResult.SolutionVariable[index].Segment;
                                    if (outseg > seg && outseg < lastseg) lp.UpdateMaximizeSegmentColumn(index);
                                }
                            }
                            lp.UpdateMaximizeSegmentDuration(segmentDuration * (lastseg - seg - 1));
                            HeapPush(lp);
                            return false;
                        }
                    }
                }
            }

            if (cooldown == Cooldown.FlameCap)
            {
                double[] manaGem = new double[segments];
                for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                {
                    if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem)
                    {
                        manaGem[calculationResult.SolutionVariable[index].Segment] += solution[index];
                    }
                }

                float manaBurn = 80;
                if (calculationOptions.AoeDuration > 0)
                {
                    Spell s = calculationResult.BaseState.GetSpell(SpellId.ArcaneExplosion);
                    manaBurn = s.CostPerSecond - s.ManaRegenPerSecond;
                }
                else if (calculationOptions.EmpoweredFireball > 0)
                {
                    Spell s = calculationResult.BaseState.GetSpell(SpellId.Fireball);
                    manaBurn = s.CostPerSecond - s.ManaRegenPerSecond;
                }
                else if (calculationOptions.EmpoweredFrostbolt > 0)
                {
                    Spell s = calculationResult.BaseState.GetSpell(SpellId.Frostbolt);
                    manaBurn = s.CostPerSecond - s.ManaRegenPerSecond;
                }
                else if (calculationOptions.SpellPower > 0)
                {
                    Spell s = calculationResult.BaseState.GetSpell(SpellId.ArcaneBlast33);
                    manaBurn = s.CostPerSecond - s.ManaRegenPerSecond;
                }
                if (icyVeinsAvailable)
                {
                    manaBurn *= 1.1f;
                }
                if (arcanePowerAvailable)
                {
                    manaBurn *= 1.1f;
                }

                // check border case if we have mana gem in first or last segment
                if (manaGem[0] > 0)
                {
                    // either no gem at 0 or make sure it starts late enough
                    int firstSeg;
                    for (firstSeg = 0; firstSeg < segments; firstSeg++)
                    {
                        if (segCount[firstSeg] > 0) break;
                    }
                    if (firstSeg < segments)
                    {
                        double totalGem = 0.0;
                        for (int seg = 0; seg < firstSeg; seg++)
                        {
                            totalGem += manaGem[seg];
                        }
                        // tfc = firstSeg * 30 + 30 - segCount[firstSeg]
                        // tgem <= tfc - 120.0 * totalGem
                        // overflow >= 2400 - tgem * manaBurn

                        // tfc - 120.0 * totalGem >= tgem >= (2400 - overflow) / manaBurn

                        // (2400 - overflow) / manaBurn <= tfc - 120.0 * totalGem
                        // 120.0 * manaBurn * totalGem - overflow <= (firstSeg * 30 + 30 - segCount[firstSeg]) * manaBurn - 2400
                        // 120.0 * totalGem - overflow / manaBurn + segCount[firstSeg] <= (firstSeg * 30 + 30) - 2400 / manaBurn

                        double overflow = solution[calculationResult.ColumnManaOverflow];

                        if (120.0 * totalGem - overflow / manaBurn + segCount[firstSeg] > (firstSeg * segmentDuration + segmentDuration) - 2400.0 * (1 + calculationResult.BaseStats.BonusManaGem) / manaBurn + eps)
                        {
                            // no gem
                            SolverLP nogem = lp.Clone();
                            if (nogem.Log != null) nogem.Log.AppendLine("No gem at 0");
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem && calculationResult.SolutionVariable[index].Segment == 0)
                                {
                                    nogem.EraseColumn(index);
                                }
                            }
                            HeapPush(nogem);
                            // restrict flame cap/overflow
                            if (lp.Log != null) lp.Log.AppendLine("Restrict flame cap with gem at 0");
                            int row = lp.AddConstraint(false);
                            lp.SetConstraintRHS(row, (firstSeg * segmentDuration + segmentDuration) - 2400.0 * (1 + calculationResult.BaseStats.BonusManaGem) / manaBurn);
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem && calculationResult.SolutionVariable[index].Segment < firstSeg) lp.SetConstraintElement(row, index, 120.0);
                                else if (calculationResult.SolutionVariable[index].Type == VariableType.ManaOverflow && calculationResult.SolutionVariable[index].Segment == 0) lp.SetConstraintElement(row, index, -1.0 / manaBurn);
                                else if (state != null && state.FlameCap && calculationResult.SolutionVariable[index].Segment == firstSeg) lp.SetConstraintElement(row, index, 1.0);
                            }
                            lp.ForceRecalculation(true);
                            HeapPush(lp);
                            return false;
                        }
                    }
                }

                if (manaGem[segments - 1] > 0)
                {
                    // either no gem or make sure it starts early enough
                    int lastSeg;
                    for (lastSeg = segments - 1; lastSeg >= 0; lastSeg--)
                    {
                        if (segCount[lastSeg] > 0) break;
                    }
                    if (lastSeg >= 0)
                    {
                        while (lastSeg > 0 && segCount[lastSeg - 1] > 0) lastSeg--;
                        double totalGem = 0.0;
                        for (int seg = lastSeg + 1; seg < segments; seg++)
                        {
                            totalGem += manaGem[seg];
                        }
                        // tfc = lastSeg * 30 + 30 - segCount[lastSeg]
                        // tgem >= tfc + 60.0 + 120 * totalGem
                        // overflow >= 2400 - fight * manaBurn + tgem * manaBurn

                        // tfc + 60.0 + 120 * totalGem <= tgem <= overflow / manaBurn - 2400 / manaBurn + fight

                        // tfc + 120 * totalGem - overflow / manaBurn <= - 2400 / manaBurn + fight - 60.0
                        // 120 * totalGem - overflow / manaBurn - segCount[lastSeg] <= fight - 90.0 - lastSeg * 30 - 2400 / manaBurn

                        double overflow = solution[calculationResult.ColumnManaOverflow + segments - 1];

                        if (120.0 * totalGem - overflow / manaBurn - segCount[lastSeg] > (calculationOptions.FightDuration - 60.0 - lastSeg * segmentDuration - segmentDuration) - 2400.0 * (1 + calculationResult.BaseStats.BonusManaGem) / manaBurn + eps)
                        {
                            // no gem
                            SolverLP nogem = lp.Clone();
                            if (nogem.Log != null) nogem.Log.AppendLine("No gem at end");
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem && calculationResult.SolutionVariable[index].Segment == segments - 1)
                                {
                                    nogem.EraseColumn(index);
                                }
                            }
                            HeapPush(nogem);
                            // restrict flame cap/overflow
                            if (lp.Log != null) lp.Log.AppendLine("Restrict flame cap with gem at end");
                            int row = lp.AddConstraint(false);
                            lp.SetConstraintRHS(row, (calculationOptions.FightDuration - 60.0 - lastSeg * segmentDuration - segmentDuration) - 2400.0 * (1 + calculationResult.BaseStats.BonusManaGem) / manaBurn);
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem && calculationResult.SolutionVariable[index].Segment > lastSeg) lp.SetConstraintElement(row, index, 120.0);
                                else if (calculationResult.SolutionVariable[index].Type == VariableType.ManaOverflow && calculationResult.SolutionVariable[index].Segment == segments - 1) lp.SetConstraintElement(row, index, -1.0 / manaBurn);
                                else if (state != null && state.FlameCap && calculationResult.SolutionVariable[index].Segment == lastSeg) lp.SetConstraintElement(row, index, -1.0);
                            }
                            lp.ForceRecalculation(true);
                            HeapPush(lp);
                            return false;
                        }
                    }
                }

                // . . . . . . X . . . . .
                // . .[. . . . X . . .]. . option 1, gem at start of X
                // . . .[. . . X . . . .]. option 2, gem at end of X
                // or anywhere in between
                // . .[. . . . X . . . .]. flamecap in this area is limited
                // Math.Ceiling(120.0 / segmentDuration) in each way
                // total area (2 * Math.Ceiling(120.0 / segmentDuration) + 1) * segmentDuration
                // max flame cap: (2 * Math.Ceiling(120.0 / segmentDuration) + 1) * segmentDuration - 240.0
                // for partial mana gems convert each 1.0 mana gem into 40 sec of extra flame cap
                int segdist = (int)Math.Ceiling(120.0 / segmentDuration);
                for (int seg = 0; seg < segments; seg++)
                {
                    if (manaGem[seg] > 0)
                    {
                        double maxfc = (2 * segdist + 1) * segmentDuration - 240.0 + (1.0 - manaGem[seg]) * 40.0;
                        double count = 0.0;
                        for (int segfc = Math.Max(seg - segdist, 0); segfc < Math.Min(seg + segdist + 1, segments); segfc++)
                        {
                            count += segCount[segfc];
                        }
                        if (count > maxfc + eps)
                        {
                            // remove mana gem
                            SolverLP nogem = lp.Clone();
                            if (nogem.Log != null) nogem.Log.AppendLine("No gem at " + seg);
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                if (calculationResult.SolutionVariable[index].Type == VariableType.ManaGem && calculationResult.SolutionVariable[index].Segment == seg)
                                {
                                    nogem.EraseColumn(index);
                                }
                            }
                            HeapPush(nogem);
                            // restrict flame cap
                            if (lp.Log != null) lp.Log.AppendLine("Restrict flame cap around " + seg);
                            int row = lp.AddConstraint(false);
                            lp.SetConstraintRHS(row, maxfc);
                            for (int index = 0; index < calculationResult.SolutionVariable.Count; index++)
                            {
                                CastingState state = calculationResult.SolutionVariable[index].State;
                                int iseg = calculationResult.SolutionVariable[index].Segment;
                                if (state != null && state.GetCooldown(cooldown) && Math.Abs(seg - iseg) <= segdist) lp.SetConstraintElement(row, index, 1.0);
                            }
                            lp.ForceRecalculation(true);
                            HeapPush(lp);
                            return false;
                        }
                    }
                }
            }

            /*double t1 = 0.0;
            double t2 = 0.0;
            double bestCoverage = 0.0;

            if (cooldownDuration < 0) cooldownDuration = 3 * segments * segmentDuration;

            for (int seg = 0; seg < segments; seg++)
            {
                double inseg = segCount[seg];
                if (inseg > 0 && (seg == 0 || segCount[seg - 1] == 0.0))
                {
                    double t = seg;
                    if (seg < segments - 1 && segCount[seg + 1] > 0.0) t = seg + 1 - inseg / segmentDuration;
                    double max = t + cooldownDuration / segmentDuration;
                    // verify that outside duration segments are 0
                    for (int outseg = seg + 1; outseg < segments; outseg++)
                    {
                        if (segCount[outseg] > 0)
                        {
                            double tt = outseg + 1 - segCount[outseg] / segmentDuration;
                            if ((outseg >= t + effectDuration / segmentDuration + eps) && (tt < max - eps))
                            {
                                // detected invalid placement undetected by original method
                                valid = false;
                                // make sure that we pairwise invalidate
                                // (outseg >= tin + effectDuration / segmentDuration && outseg < (int)(tin + cooldownDuration / segmentDuration)
                                // outseg + 1 <= tin + cooldownDuration / segmentDuration
                                // outseg - effectDuration / segmentDuration >= tin >= outseg + 1 - cooldownDuration / segmentDuration
                                // cooldownDuration >= effectDuration + 2 * segmentDuration !!! if this isn't true then we have problems, means segmentDuration has to be small enough, 30 sec = good
                                // (seg >= tout + (effectDuration - cooldownDuration) / segmentDuration && seg < (int)tout)
                                // seg + 1 <= tout <= seg + (cooldownDuration - effectDuration) / segmentDuration
                                double tin = t;
                                double tout = tt;
                                if (tin < outseg + 1 - cooldownDuration / segmentDuration) tin = outseg + 1 - cooldownDuration / segmentDuration;
                                if (tout > seg + (cooldownDuration - effectDuration) / segmentDuration) tout = seg + (cooldownDuration - effectDuration) / segmentDuration;
                                double c1 = 0.0;
                                double c2 = 0.0;
                                for (int s = 0; s < segments; s++)
                                {
                                    if ((s >= tin + (effectDuration - cooldownDuration) / segmentDuration - eps && s + 1 < tin - eps) || (s >= tin + effectDuration / segmentDuration + eps && s + 1 < tin + cooldownDuration / segmentDuration + eps))
                                    {
                                        c1 += segCount[s];
                                    }
                                    if ((s >= tout + (effectDuration - cooldownDuration) / segmentDuration - eps && s + 1 < tout - eps) || (s >= tout + effectDuration / segmentDuration + eps && s + 1 < tout + cooldownDuration / segmentDuration + eps))
                                    {
                                        c2 += segCount[s];
                                    }
                                }
                                double coverage = Math.Min(c1, c2);
                                if (coverage < eps)
                                {
                                    coverage = 0.0; // troubles
                                }
                                if (coverage > bestCoverage)
                                {
                                    t1 = tin;
                                    t2 = tout;
                                    bestCoverage = coverage;
                                }
                            }
                        }
                    }
                }
            }
            /*if (!valid)
            {
                //if (lp.disabledHex == null) lp.disabledHex = new int[CooldownMax];
                // branch on whether t1 or t2 is active, they can't be both
                CompactLP t1active = lp.Clone();
                // cooldown used
                //t1active.Log += "Use " + cooldown.ToString() + " at " + t1 + ", disable around\r\n";
                for (int outseg = 0; outseg < segments; outseg++)
                {
                    if ((outseg >= t1 + (effectDuration - cooldownDuration) / segmentDuration - eps && outseg + 1 < t1 - eps) || (outseg >= t1 + effectDuration / segmentDuration + eps && outseg + 1 < t1 + cooldownDuration / segmentDuration + eps))
                    {
                        for (int index = outseg * statsList.Count * spellList.Count + colOffset - 1; index < (outseg + 1) * statsList.Count * spellList.Count + colOffset - 1; index++)
                        {
                            CharacterCalculationsMage stats = calculatedStats.SolutionStats[index];
                            if (stats != null && stats.GetCooldown(cooldown)) t1active.EraseColumn(index);
                        }
                    }
                }
                HeapPush(t1active);
                // cooldown not used
                //lp.Log += "Use " + cooldown.ToString() + " at " + t2 + ", disable around\r\n";
                for (int outseg = 0; outseg < segments; outseg++)
                {
                    if ((outseg >= t2 + (effectDuration - cooldownDuration) / segmentDuration - eps && outseg + 1 < t2 - eps) || (outseg >= t2 + effectDuration / segmentDuration + eps && outseg + 1 < t2 + cooldownDuration / segmentDuration + eps))
                    {
                        for (int index = outseg * statsList.Count * spellList.Count + colOffset - 1; index < (outseg + 1) * statsList.Count * spellList.Count + colOffset - 1; index++)
                        {
                            CharacterCalculationsMage stats = calculatedStats.SolutionStats[index];
                            if (stats != null && stats.GetCooldown(cooldown)) lp.EraseColumn(index);
                        }
                    }
                }
                HeapPush(lp);

                return false;
            }*/

            return valid;
        }

        private bool ValidateSCB(Cooldown trinket)
        {
            double[] trinketCount = new double[segments];
            double[] flamecapCount = new double[segments];
            for (int outseg = 0; outseg < segments; outseg++)
            {
                double s = 0.0;
                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.GetCooldown(trinket))
                    {
                        s += solution[index];
                    }
                }
                trinketCount[outseg] = s;
            }
            for (int outseg = 0; outseg < segments; outseg++)
            {
                double s = 0.0;
                for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                {
                    CastingState state = calculationResult.SolutionVariable[index].State;
                    if (state != null && state.FlameCap)
                    {
                        s += solution[index];
                    }
                }
                flamecapCount[outseg] = s;
            }
            int rightdist = ((int)Math.Floor((120.0 - 15.0) / segmentDuration));
            int leftdist = ((int)Math.Floor((180.0 - 60.0) / segmentDuration));

            bool valid = true;
            int flamecapSeg = 0;
            int trinketSeg = 0;
            int minDist = int.MaxValue;

            for (int seg = 0; seg < segments; seg++) // trinket
            {
                double inseg = trinketCount[seg];
                if (inseg > 0)
                {
                    for (int outseg = 0; outseg < segments; outseg++) // flamecap
                    {
                        if ((outseg > seg - leftdist) || (outseg < seg + rightdist))
                        {
                            if (flamecapCount[outseg] > 0)
                            {
                                valid = false;
                                if (Math.Abs(seg - outseg) < minDist)
                                {
                                    trinketSeg = seg;
                                    flamecapSeg = outseg;
                                    minDist = Math.Abs(seg - outseg);
                                }
                            }
                        }
                    }
                }
            }
            if (!valid)
            {
                // branch on whether trinket is used or flame cap is used
                SolverLP trinketUsed = lp.Clone();
                // flame cap used
                //lp.Log += "Disable " + trinket.ToString() + " close to " + flamecapSeg + "\r\n";
                for (int inseg = 0; inseg < segments; inseg++)
                {
                    if ((inseg > flamecapSeg - rightdist) || (inseg < flamecapSeg + leftdist))
                    {
                        for (int index = segmentColumn[inseg]; index < segmentColumn[inseg + 1]; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.GetCooldown(trinket)) lp.EraseColumn(index);
                        }
                    }
                }
                HeapPush(lp);
                // trinket used
                //trinketUsed.Log += "Disable Flame Cap close to " + trinketSeg + "\r\n";
                for (int outseg = 0; outseg < segments; outseg++)
                {
                    if ((outseg > trinketSeg - leftdist) || (outseg < trinketSeg + rightdist))
                    {
                        for (int index = segmentColumn[outseg]; index < segmentColumn[outseg + 1]; index++)
                        {
                            CastingState state = calculationResult.SolutionVariable[index].State;
                            if (state != null && state.FlameCap) trinketUsed.EraseColumn(index);
                        }
                    }
                }
                HeapPush(trinketUsed);
                return false;
            }
            return valid;
        }
    }
}
