/*
        if (passedCheckpoint) {
            AddReward(PassCheckpointReward);
        }
        */

        // Add rewards if the agent is heading in the right direction
        // AddReward(distanceToCheckpoint * TowardsCheckpointReward);
        // AddReward(kart.LocalSpeed() * SpeedReward);

        /*
        for (int i=0; i<distanceToWall.Count; i++) {
            if (distanceToWall[i] < 0.2f) {
                AddReward(HitPenalty);
            }
        }
        */

        if (isOutBound) {
            // AddReward(HitPenalty);
        }